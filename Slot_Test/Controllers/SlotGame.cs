using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slot_Test.Entities;
using Slot_Test.Repositories.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slot_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotGame : ControllerBase
    {
        private readonly WebApiContext context;
        private readonly DbSet<User> dbSet;

        public SlotGame(WebApiContext context)
        {
            this.context = context;
            dbSet = context.Set<User>();
        }

        [HttpGet]
        [Route("spin")]
        public async Task<IActionResult> Spin(int betAmount, Guid id)
        {
            var result = SimulateSpin(betAmount);
            var user = await dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
            user.Wallet -= betAmount;
            user.Wallet += result.Win;
            result.UserId = user.Id;
            result.Value = betAmount;
            var addedSession = context.Session.Add(result);
            await context.SaveChangesAsync();

            return Ok(result);
        }

       

        [HttpGet]
        [Route("calculate-rtp")]
        public IActionResult Rtp(int numberOfSpins)
        {
            double totalWin = 0;
            for (int i = 0; i < numberOfSpins; i++)
            {
                var session = SimulateSpin(1);
                totalWin += session.Win;
            }

            return Ok($"The RTP for {numberOfSpins} spins is {totalWin/numberOfSpins}");
        }

        private Session SimulateSpin(int betAmount)
        {
            string[] reelOne = new string[] { "Horseshoe", "Bell", "Horseshoe", "Spade", "Horseshoe", "Diamond", "Horseshoe", "Spade", "Horseshoe", "Heart", "Horseshoe", "Bell"};
            string[] reelTwo = new string[] { "Horseshoe", "Bell", "Horseshoe", "Spade", "Horseshoe", "Diamond", "Horseshoe", "Spade", "Horseshoe", "Heart", "Horseshoe", "Bell"};
            string[] reelThree = new string[] { "Diamond", "Bell", "Diamond", "Star", "Spade", "Bell", "Diamond", "Heart", "Star", "Spade", "Diamond", "Bell"};
            Random rd = new Random();
            Strips strip = new Strips
            {
                ReelOne = rd.Next(1, 11),
                ReelTwo = rd.Next(1, 11),
                ReelThree = rd.Next(1, 11)
            };

            Session spinSession = new Session
            {
                WinningCombination = reelOne[strip.ReelOne] + " " + reelTwo[strip.ReelTwo] + " " + reelThree[strip.ReelThree],
                UpperCombination = reelOne[strip.ReelOne - 1] + " " + reelTwo[strip.ReelTwo - 1] + " " + reelThree[strip.ReelThree - 1],
                LowerCombination = reelOne[strip.ReelOne + 1] + " " + reelTwo[strip.ReelTwo + 1] + " " + reelThree[strip.ReelThree + 1]
            };


            switch (reelOne[strip.ReelOne])
            {
                case "Bell" when reelTwo[strip.ReelTwo] == "Bell" && reelThree[strip.ReelThree] == "Bell":
                    spinSession.Multiplier = 20;
                    break;
                case "Heart" when reelTwo[strip.ReelTwo] == "Heart" && reelThree[strip.ReelThree] == "Heart":
                    spinSession.Multiplier = 16;
                    break;
                case "Diamond" when reelTwo[strip.ReelTwo] == "Diamond" && reelThree[strip.ReelThree] == "Diamond":
                    spinSession.Multiplier = 12;
                    break;
                case "Spade" when reelTwo[strip.ReelTwo] == "Spade" && reelThree[strip.ReelThree] == "Spade":
                    spinSession.Multiplier = 8;
                    break;
                case "Horseshoe" when reelTwo[strip.ReelTwo] == "Horseshoe" && reelThree[strip.ReelThree] == "Star":
                    spinSession.Multiplier = 4;
                    break;
                case "Horseshoe" when reelTwo[strip.ReelTwo] == "Horseshoe" && reelThree[strip.ReelThree] != "Star":
                    spinSession.Multiplier = 2;
                    break;
                default:
                    spinSession.Multiplier = 0;
                    break;
            }

            spinSession.Win = betAmount * spinSession.Multiplier;

            return spinSession;
        }
    }
}
