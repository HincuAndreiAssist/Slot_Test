using Microsoft.AspNetCore.Mvc;
using Slot_Test.Entities;
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
        
        public SlotGame()
        {
            
        }

        [HttpGet]
        [Route("spin")]
        public IActionResult Spin(int betAmount)
        {
            return Ok(SimulateSpin(betAmount));
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
