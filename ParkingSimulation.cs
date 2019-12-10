using System;
using System.Collections.Generic;

namespace ParkingLotSimulation
{
    internal class ParkingSimulation
    {


        public static List<ParkingSlot> ParkingSlots;

        private static void Main(string[] args)
        {

            Console.WriteLine("Two Wheeler Parking Slots : ");
            int TwoWheelerParkingSlots = ParkingHelper.ReadData();

            Console.WriteLine("Four Wheeler Parking Slots : ");
            int FourWheelerParkingSlots = ParkingHelper.ReadData();

            Console.WriteLine("Heavy Vehicle Parking Slots : ");
            int HeavyVehicleParkingSlots = ParkingHelper.ReadData();

            ParkingHelper.InitializeParkingLot();

            List<Ticket> Tickets = new List<Ticket>();


            for (int i = 0; i < TwoWheelerParkingSlots; i++)
            {
                ParkingSlot ParkingSlot = new ParkingSlot((int)ParkingHelper.VehicleType.TwoWheeler, (i + 1));

                ParkingSlots.Add(ParkingSlot);
            }

            for (int i = 0; i < FourWheelerParkingSlots; i++)
            {
                ParkingSlot ParkingSlot = new ParkingSlot((int)ParkingHelper.VehicleType.FourWheeler, (TwoWheelerParkingSlots + i + 1));

                ParkingSlots.Add(ParkingSlot);
            }

            for (int i = 0; i < HeavyVehicleParkingSlots; i++)
            {
                ParkingSlot ParkingSlot = new ParkingSlot((int)ParkingHelper.VehicleType.HeavyVehicle, (TwoWheelerParkingSlots + FourWheelerParkingSlots + i + 1));

                ParkingSlots.Add(ParkingSlot);
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
                    Tickets = ParkingHelper.GenerateTicket(Tickets);
                    ParkingHelper.PrintTicket(Tickets);
                }

                else if (choice == 2)
                {

                    switch (ParkingHelper.RevokeMenu())
                    {
                        case 1:
                            Tickets = ParkingHelper.RevokeTicketWithTicketID(Tickets);
                            break;

                        case 2:
                            Tickets = ParkingHelper.RevokeTicketWithVehicleNumber(Tickets); ;
                            break;
                    }

                    ParkingHelper.PrintTicket(Tickets);
                }

                else
                {
                    break;
                }
            }
        }
    }
}
