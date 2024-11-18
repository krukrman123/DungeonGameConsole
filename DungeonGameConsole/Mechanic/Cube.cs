using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Mechanic
{
    public class Cube
    {

        private Random random;
        private int countSten;


        public Cube()
        {
            countSten = 6;
            random = new Random();


        }


        public Cube(int countSten)
        {
            this.countSten = countSten;
            random = new Random();
        }

        public int ReturnCountSten()
        {
            return countSten;
        }

        public int ThrowIt()
        {
            return random.Next(1, countSten + 1);
        }

        public override string ToString()
        {
            return String.Format("Kostka s {0} stěnami", countSten);
        }


    }
    }
