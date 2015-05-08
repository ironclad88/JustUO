using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Custom
{
    class RandomClass
    {

        Random r = new Random();

        public int D6Roll(int Rolls)
        {
        int sum=0;
        for (int i = 0; i < 4; i++)
        {
            var roll = r.Next(1, 7);
            sum += roll;
        }
        return sum;
        }

        public int D8Roll(int Rolls)
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                var roll = r.Next(1, 9);
                sum += roll;
            }
            return sum;
        }

        public int D10Roll(int Rolls)
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                var roll = r.Next(1, 11);
                sum += roll;
            }
            return sum;
        }

        public int D12Roll(int Rolls)
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                var roll = r.Next(1, 13);
                sum += roll;
            }
            return sum;
        }

        public int D20Roll(int Rolls)
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                var roll = r.Next(1, 21);
                sum += roll;
            }
            return sum;
        }
    }
}
