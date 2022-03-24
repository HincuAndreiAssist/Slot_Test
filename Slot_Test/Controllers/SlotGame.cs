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
            Strips strip = new Strips
            {
                ReelOne = randomReelOne(),
                ReelTwo = randomReelTwo(),
                ReelThree = randomReelThree()
            };

            Session spinSession = new Session
            {
                Combination = strip.ReelOne + " " + strip.ReelTwo + " " + strip.ReelThree
            };


            switch (strip.ReelOne)
            {
                case "Bell" when strip.ReelTwo == "Bell" && strip.ReelThree == "Bell":
                    spinSession.Multiplier = 20;
                    break;
                case "Heart" when strip.ReelTwo == "Heart" && strip.ReelThree == "Heart":
                    spinSession.Multiplier = 16;
                    break;
                case "Diamond" when strip.ReelTwo == "Diamond" && strip.ReelThree == "Diamond":
                    spinSession.Multiplier = 12;
                    break;
                case "Spade" when strip.ReelTwo == "Spade" && strip.ReelThree == "Spade":
                    spinSession.Multiplier = 8;
                    break;
                case "Horseshoe" when strip.ReelTwo == "Horseshoe" && strip.ReelThree == "Star":
                    spinSession.Multiplier = 4;
                    break;
                case "Horseshoe" when strip.ReelTwo == "Horseshoe" && strip.ReelThree != "Star":
                    spinSession.Multiplier = 2;
                    break;
                default:
                    spinSession.Multiplier = 0;
                    break;
            }

            spinSession.Win = betAmount * spinSession.Multiplier;

            return spinSession;
        }

        private string randomReelThree()
        {
            Random rd = new Random();
            var randomNumber = rd.Next(0, 100);

            switch (randomNumber)
            {
                case int n when (n < 20):
                    return "Bell";
                case int n when (n < 30):
                    return "Heart";
                case int n when (n < 60):
                    return "Diamond";
                case int n when (n < 80):
                    return "Spade";
                default:
                    return "Star";
            }
        }

        private string randomReelTwo()
        {
            Random rd = new Random();
            var randomNumber = rd.Next(0, 100);

            switch (randomNumber)
            {
                case int n when (n < 10):
                    return "Bell";
                case int n when (n < 20):
                    return "Heart";
                case int n when (n < 30):
                    return "Diamond";
                case int n when (n < 50):
                    return "Spade";
                default:
                    return "Horseshoe";
            }
        }

        private string randomReelOne()
        {
            Random rd = new Random();
            var randomNumber = rd.Next(0, 100);
    
            switch (randomNumber)
            {
                case int n when (n < 10):
                    return "Bell";
                case int n when (n < 20):
                    return "Heart";
                case int n when (n < 30):
                    return "Diamond";
                case int n when (n < 50):
                    return "Spade";
                default:
                    return "Horseshoe";
            }
        }
    }
}
