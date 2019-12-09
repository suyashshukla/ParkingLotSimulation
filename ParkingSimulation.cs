using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    internal class ParkingSimulation
    {

        public enum VehicleType
        {
            TWO = 0,
            FOUR = 1
        }

        public static List<ParkingLot> parkingLots;

        private static void Main(string[] args)
        {

            Console.WriteLine("Two Wheeler Parking Lots : ");
            int M = int.Parse(Console.ReadLine());

            Console.WriteLine("Four Wheeler Parking Lots : ");
            int N = int.Parse(Console.ReadLine());

            parkingLots = new List<ParkingLot>();
            List<Ticket> tickets = new List<Ticket>();

            for (int i = 0; i < M; i++)
            {
                ParkingLot parking = new ParkingLot((int)VehicleType.TWO, (i + 1));

                parkingLots.Add(parking);
            }

            for (int i = 0; i < N; i++)
            {
                ParkingLot parking = new ParkingLot((int)VehicleType.FOUR, (M + i + 1));

                parkingLots.Add(parking);
            }


            while (true)
            {

                ParkingHelper.ShowParkingStatus();


                Console.WriteLine();
                Console.WriteLine("1. PARK Vehicle");
                Console.WriteLine("2. UNPARK Vehicle");
                Console.WriteLine("3. Exit");
                Console.WriteLine();

                int choice = int.Parse(Console.ReadLine());

                Console.WriteLine();

                if (choice == 1)
                {
                    tickets = ParkingHelper.GenerateTicket(tickets);
                    Console.WriteLine("********************************************");
                    ParkingHelper.PrintTicket(tickets);
                    Console.WriteLine("********************************************");
                }

                else if (choice == 2)
                {
                    tickets = ParkingHelper.RevokeTicket(tickets);
                    Console.WriteLine("********************************************");
                    ParkingHelper.PrintTicket(tickets);
                    Console.WriteLine("********************************************");
                }

                else
                {
                    break;
                }
            }
        }
    }
}
