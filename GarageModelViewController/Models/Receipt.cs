namespace GarageModelViewController.Models
{
    public class Receipt // OBS! Använder inte längre denna klass, utan istället ReceiptView Model! 
    {
        private int price;
        //RegNr,in/ut-checkningstid, total parkerings period och pris automatiskt efter att en bil checkatsut. 
        public int? Id { get; set; }
        public DateTime Ankomsttid { get; set; }
        public DateTime Now { get; set; } = DateTime.Now;
        public TimeSpan ParkedTime { get; set; }

       
        public int Price
        {
            get { return 35; }
            private set{ price = this.Price; }
        }


        public int TotalPrice { get { return this.Price+(this.Price * (int)this.ParkedTime.TotalHours); } private set { price = 10;  } } //Eventuellt räkna ut priset här här! 

        public string RegistrationNummber { get; set; }

        




    }
}
