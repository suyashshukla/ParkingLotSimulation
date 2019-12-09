using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLot
{
    internal class ParkingHelper
    {


        public static void ShowParkingStatus()
        {
            foreach (ParkingLot lot in ParkingSimulation.parkingLots)
            {
                Console.WriteLine("Parking Lot : " + lot.Id + " for "+lot.GetWheels()+" is " + (lot.Status ? "VACANT" : "OCCUPIED"));
            }
        }

        public static List<Ticket> GenerateTicket(List<Ticket> tickets)
        {

            Console.WriteLine("Vehicle Number");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Vehicle Type");
            int type = int.Parse(Console.ReadLine());

            Vehicle vehicle = new Vehicle(number, type);

            IEnumerable<ParkingLot> slots = from lots in ParkingSimulation.parkingLots
                                            where (vehicle.Type == lots.Type) && lots.IsEmpty()
                                            select lots;

            if (slots.Count() == 0)
            {
                Console.WriteLine("Slots Full\n");
            }

            foreach (ParkingLot lot in slots)
            {
                lot.ParkVehicle(vehicle);

                ParkingSimulation.parkingLots.Remove(ParkingSimulation.parkingLots.Where(l => l.Id == lot.Id).Single());
                ParkingSimulation.parkingLots.Add(lot);

                Ticket ticket = new Ticket();
                ticket.AddSlot(lot.Id);
                ticket.AddTimeIn(DateTime.Now.ToString());
                ticket.AddId(tickets.Count());

                tickets.Add(ticket);

                break;
            }
            return tickets;
        }

        public static List<Ticket> RevokeTicket(List<Ticket> tickets)
        {
            Console.WriteLine("Ticket ID : ");
            int id = int.Parse(Console.ReadLine());

            IEnumerable<Ticket> ticket = from receipt in tickets
                                         where receipt.Id == id
                                         select receipt;

            Ticket t = ticket.Single();

            t.AddTimeOut(DateTime.Now.ToString());
            
            IEnumerable<ParkingLot> parkingLot = from park in ParkingSimulation.parkingLots
                                                 where park.Id == t.Slot
                                                 select park;

            ParkingLot parking = parkingLot.First();
                                    
            ParkingSimulation.parkingLots.Remove(parking);

            parking.UnparkVehicle();

            ParkingSimulation.parkingLots.Add(parking);

            return tickets;
        }

        public static void PrintTicket(List<Ticket> ticketList)
        {
            
            foreach(Ticket t in ticketList)
            {
                Console.WriteLine("Ticket ID # : " + t.Id);
                Console.WriteLine("In Time : " + t.InTime);
                Console.WriteLine("Out Time : " + t.OutTime);
                Console.WriteLine("Parking Lot # : " + t.Slot);
                Console.WriteLine(t.OutTime.Length > 1 ? "INACTIVE" : "ACTIVE");
                Console.WriteLine();
            }

        }


    }
}
