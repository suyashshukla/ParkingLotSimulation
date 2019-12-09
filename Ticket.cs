namespace ParkingLot
{
    internal class Ticket
    {

        public int Id { get; set; }

        public string InTime { get; set; }

        public string OutTime { get; set; }
        
        public int Slot { get; set; }

        public Ticket()
        {
            OutTime = "-";
        }

        public void AddSlot(int slot)
        {
            Slot = slot;
        }
        
        public void AddTimeIn(string intime)
        {
            InTime = intime;
        }

        public void AddTimeOut(string outtime)
        {
            OutTime = outtime;
        }

        public void AddId(int id)
        {
            this.Id = id;
        }

    }

}
