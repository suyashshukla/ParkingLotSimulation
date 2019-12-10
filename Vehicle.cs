namespace ParkingLotSimulation
{
    internal class Vehicle
    {

        public Vehicle(int number, int type)
        {
            Number = number;
            Type = type;
        }

        public int Number { get; set; }

        public int Type { get; set; }

    }
}
