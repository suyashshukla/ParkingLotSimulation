using System;

namespace ParkingLotSimulation
{
    internal class ParkingSlot : IComparable<ParkingSlot> 
    {

        public ParkingSlot(int type, int id)
        {
            Type = type;
            Status = true;
            Id = id;
        }

        public bool Status { get; set; }

        public int Id { get; set; }

        public int Type { get; set; }

        public int Wheels { get; set; }

        public Vehicle Vehicle { get; set; }

        public void ParkVehicle(Vehicle vehicle)
        {
            Status = false;
            Vehicle = vehicle;
        }

        public void UnparkVehicle()
        {
            Status = true;
            Vehicle = null;
        }

        public bool IsEmpty()
        {
            return Status;
        }
        
        public string GetVehicleType()
        {
            return Type == 0 ? "TWO   WHEELER" : Type == 1 ? "FOUR  WHEELER" : "HEAVY VEHICLE";
        }

        public string GetParkedVehicleType()
        {
            return Vehicle.Type==0 ? "TWO   WHEELER" : Vehicle.Type == 1 ? "FOUR  WHEELER" : "HEAVY VEHICLE";
        }

        public int CompareTo(ParkingSlot obj)
        {
            return Id.CompareTo(obj.Id);
        }
    }
}
