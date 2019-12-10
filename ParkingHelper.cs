using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotSimulation
{
    internal class ParkingHelper
    {

        public enum VehicleType
        {
            TwoWheeler = 0,
            FourWheeler = 1,
            HeavyVehicle = 2
        }


        public static void ShowParkingStatus()
        {
            ParkingSimulation.ParkingSlots.Sort();

            foreach (ParkingSlot slot in ParkingSimulation.ParkingSlots)
            {
                Console.WriteLine("Parking Slot : " + slot.Id + " for "
                    + slot.GetVehicleType() +
                    " is " + (slot.IsEmpty() ? "VACANT" : "OCCUPIED")
                    + (!slot.IsEmpty() ? " by a "
                    + slot.GetParkedVehicleType() : ""));
            }
        }



        public static int ReadData()
        {
            return int.Parse(Console.ReadLine());
        }



        public static void InitializeParkingLot()
        {
            ParkingSimulation.ParkingSlots = new List<ParkingSlot>();
        }



        public static List<Ticket> GenerateTicket(List<Ticket> tickets)
        {

            Console.WriteLine("Vehicle Number");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Vehicle Type");
            int type = int.Parse(Console.ReadLine());

            Vehicle vehicle = new Vehicle(number, type);

            IEnumerable<ParkingSlot> EmptyParkingSlots = from slots in ParkingSimulation.ParkingSlots
                                                         where (vehicle.Type == slots.Type) && slots.IsEmpty()
                                                         select slots;

            ParkingSlot Slot;

            if (EmptyParkingSlots.Count() == 0)
            {
                Slot = ChangeSlotConfiguration(type);
                if (Slot == null)
                {
                    Console.WriteLine("Slots Full");
                    return tickets;
                }
            }

            else
            {
                Slot = EmptyParkingSlots.First();
            }

            Slot.ParkVehicle(vehicle);

            return IssueTicket(tickets, Slot);
        }



        public static List<Ticket> IssueTicket(List<Ticket> tickets, ParkingSlot Slot)
        {
            Ticket ticket = new Ticket();
            ticket.AddSlot(Slot.Id);
            ticket.AddTimeIn(DateTime.Now.ToString());
            ticket.AddId(tickets.Count());

            tickets.Add(ticket);

            return tickets;
        }






        public static List<Ticket> RevokeTicketWithTicketID(List<Ticket> tickets)
        {
            Console.WriteLine("Ticket ID : ");
            int id = ReadData();

            Ticket ticket;

            try
            {
                ticket = (from receipt in tickets
                          where receipt.Id == id
                          select receipt).Single();
            }

            catch (Exception)
            {
                Console.WriteLine("Invalid Entry");
                return tickets;
            }

            ticket.AddTimeOut(DateTime.Now.ToString());

            ParkingSlot parkingSlot = (from park in ParkingSimulation.ParkingSlots
                                       where park.Id == ticket.Slot
                                       select park).Single();

            parkingSlot.UnparkVehicle();

            return tickets;
        }







        public static void PrintTicket(List<Ticket> TicketList)
        {
            Console.WriteLine();

            Ticket t = TicketList[TicketList.Count() - 1];

                Console.WriteLine("___________________________________");
                Console.WriteLine("Ticket ID # : " + t.Id);
                Console.WriteLine("In Time : " + t.InTime);
                Console.WriteLine("Out Time : " + t.OutTime);
                Console.WriteLine("Parking Lot # : " + t.Slot);
                Console.WriteLine(t.OutTime.Length > 1 ? "INACTIVE" : "ACTIVE");
                Console.WriteLine("___________________________________");
                Console.WriteLine();
            
        }






        public static List<Ticket> RevokeTicketWithVehicleNumber(List<Ticket> tickets)
        {
            Console.WriteLine("Vehicle Number : ");
            int number = int.Parse(Console.ReadLine());


            ParkingSlot parkingSlot = (from slots in ParkingSimulation.ParkingSlots
                                       where slots.Vehicle.Number == number
                                       select slots)
                                       .Single();

            Ticket ticket = (from ticketValue in tickets
                             where ticketValue.Slot == parkingSlot.Id
                             select ticketValue)
                             .Single();

            ticket.AddTimeOut(DateTime.Now.ToString());

            parkingSlot.UnparkVehicle();

            return tickets;
        }





        public static int RevokeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Through Ticket ID");
            Console.WriteLine("2. Through Vehicle Number");
            int choice = ReadData();

            return choice;
        }





        public static ParkingSlot ChangeSlotConfiguration(int VehicleConfiguration)
        {
            IEnumerable<ParkingSlot> ParkingSlots;
            ParkingSlot Slot = null;


            switch (VehicleConfiguration)
            {
                case (int)VehicleType.HeavyVehicle:
                    Console.WriteLine("Regret! Slots Full");
                    break;

                case (int)VehicleType.FourWheeler:
                    ParkingSlots = from slots in ParkingSimulation.ParkingSlots
                                   where (slots.Type == (int)VehicleType.HeavyVehicle) && slots.IsEmpty()
                                   select slots;
                    if (ParkingSlots.Count() == 0)
                    {
                        Console.WriteLine("Regret! Slots Full");
                    }
                    else
                    {
                        Slot = ParkingSlots.First();
                    }
                    break;
                case (int)VehicleType.TwoWheeler:
                    ParkingSlots = from slots in ParkingSimulation.ParkingSlots
                                   where (slots.Type == (int)VehicleType.FourWheeler || slots.Type == (int)VehicleType.HeavyVehicle) && slots.IsEmpty()
                                   select slots;
                    if (ParkingSlots.Count() == 0)
                    {
                        Console.WriteLine("Regret! Slots Full");
                    }
                    else
                    {
                        Slot = ParkingSlots.First();
                    }

                    break;

            }

            return Slot;
        }

    }
}
