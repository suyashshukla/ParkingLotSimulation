namespace ParkingLot
{
    internal class ParkingLot
    {

        public ParkingLot(int type, int id)
        {
            Type = type;
            Status = true;
            Id = id;
        }

        public bool Status { get; set; }

        public int Id { get; set; }

        public int Type { get; set; }

        public Vehicle Vehicle { get; set; }

        public void ParkVehicle(Vehicle vehicle)
        {
            Status = false;
            Vehicle = vehicle;
        }

        public void UnparkVehicle()
        {
            this.Status = true;
        }

        public bool IsEmpty()
        {
            return this.Status;
        }

        public string GetWheels()
        {
            return Type==0?"TWO WHEELER":"FOUR WHEELER";
        }

    }
}
