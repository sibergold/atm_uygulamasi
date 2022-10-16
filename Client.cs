namespace atm_uygulamasi
{
    public class Client
     {
        public string Name = "";
        public string Password = "";
        public double Balance = 0;
        public Client(string name , string password)
        {
            this.Name = name;
            this.Password = password;
        }
        public Client(string name , string password , double initial_Balance)
        {
            this.Name = name;
            this.Password = password;
            this.Balance = initial_Balance;
        }
    }
}