using System;
 namespace atm_uygulamasi
 {
     class MainClass
     {
       
        static short gunluk_Cekme_limiti = 10000;
        static List <Client> clients = new List<Client>()
        {
            new Client("Hızır Çakırbeyli","695087684",5000000)
        };
        static List<string> islemler = new List<string>()
        {
            "Para çekme","Para yatırma","Bakiye sorgulama","Hesaptan çık","Uygulamadan çık","Gün sonu"
        };
        static Client client = new Client("","",0);
        static void Main(string[] args)
        {
            Giris_Yap();
            islem_secimi:
            Console.WriteLine("Bir işlem seçin.");
            for (int i = 0; i < islemler.Count; i++)
            {
                Console.WriteLine("{0}  {1}", i + 1 , islemler[i]);
            }

            switch (try_short_input((short)islemler.Count))
            {
                case 1:
                    Withdraw(ref client.Balance);
                break;
                case 2:
                    Deposit(ref client.Balance);
                break;
                case 3:
                    ShowBalance(client.Balance);
                break;
                case 4:
                    Main(new string[0]);
                break;
                case 5:
                    Environment.Exit(0);
                break;
                case 6:
                    Gun_Sonu();
                break;
            }
            goto islem_secimi;
        }
        
        static void Withdraw(ref double balance){
            Console.WriteLine("Çekmek istediğin miktarı gir.");
            short Withdrawability = gunluk_Cekme_limiti;
            if (balance < gunluk_Cekme_limiti){
                Withdrawability = (short)balance;
            }
            short amount = try_short_input(Withdrawability);
            if (amount > 0){
                balance -= amount;
                Console.WriteLine("Para çekme işlemi başarıyla tamamlandı.");
                Console.WriteLine("___________________________________________");
                Console.WriteLine();
                Logger.LogMaker("ATM'den "+client.Name+" tarafından "+ amount +" TL çekildi");
            }
        }
        static void Deposit(ref double balance){
            Console.WriteLine("Yatırmak istediğin miktarı gir.");
            short amount = try_short_input(short.MaxValue);
            if (amount >0){
                balance += amount;
                Console.WriteLine("Para yatırma işlemi başarıyla tamamlandı.");
                Console.WriteLine("___________________________________________");
                Console.WriteLine();
                Logger.LogMaker("ATM'ye "+client.Name+" tarafından "+ amount +" TL yatırıldı");
            }
        }
        static void ShowBalance(double balance){
            Console.WriteLine("Bakiyeniz : " + balance.ToString("F3"));
            Console.WriteLine("___________________________________________");
            Console.WriteLine();
        }
        static void Gun_Sonu(){
            Logger.LogShower();
        }
        static short try_short_input(short limit)
        {
            short input = 0;
            void geri_don(){Console.WriteLine("geri dönmek için 'Enter' tuşuna basınız."); 
            Console.ReadKey();}
            try_input_();
            void try_input_(){  
                bool Try = Int16.TryParse(Console.ReadLine(),out input);
                if (input > limit && limit > client.Balance){Console.WriteLine("Yetersiz bakiye.");geri_don();}
                else if (input > limit && limit < client.Balance){Console.WriteLine("Günlük para çekme limiti "+ gunluk_Cekme_limiti +" TL'dir.");
                geri_don(); }
                else if (input < 0){Console.WriteLine("Hatalı tuşlama."); geri_don(); }
            }
            return input;
        }
        static string try_input()
        {
            string input = Console.ReadLine();
            if (input == ""){Console.WriteLine("Boş bırakılamaz.");  
            try_input();}
            return input;
        }
        static string try_input_username()
        {
            string input = Console.ReadLine();
            foreach (Client one in clients)
            {
                if (input == one.Name)
                {
                    Console.WriteLine("Zaten kayıtlısın.");
                    Console.WriteLine("Ana menüye dönmek için herhangi bir tuşa basın.");
                    Console.ReadKey();
                    Main(new string[0]); 
                }
            }
            if (input == ""){Console.WriteLine("Boş bırakılamaz.");  try_input_username();}
            return input;
        }
        static Client Kayit_Ol(){
            Console.WriteLine("Adınızı girin.");
            string clientname = try_input_username();
            Console.WriteLine("şifre girin.");
            string password = try_input();

            clients.Add(new Client(clientname,password));
            return new Client(clientname,password);
        }
        static void Giris_Yap(){
            Console.Write("Lütfen Kullanıcı adınızı girin.");
            string client_input = try_input();
            string password_input ="";
            int counter = 0;
            foreach (var client in clients)
            {
                if (client.Name != client_input)
                    {counter ++;}
                else break;
            }
            if (counter == clients.Count){
                Console.WriteLine("Kullanıcı bulunamadı.");
                Console.WriteLine("Kayıt olmak ister misin ?  (Evet için  e tuşuna, Hayır için  h tuşuna basın.");
                if (Console.ReadLine() == "e")
                    client = Kayit_Ol(); 

                else Environment.Exit(0);
            }
            
            else {
                tryingToTypePassword:
                Console.Write("Lütfen şifrenizi girin. ");
                password_input = try_input();
                    if (clients[counter].Password == password_input)
                    {
                        Console.WriteLine("Giriş başarılı.");
                        client = clients[counter];
                    }
                    else                                        
                    {
                        Logger.LogMaker(clients[counter].Name + " isimli kullanıcı şifeyi yanlış girdi. ");
                        Console.WriteLine("Şifre yanlış.");
                        Console.WriteLine("işlemi sonlandırmak için ç tuşuna , yeniden denemek için Enter tuşuna basın.");
                        if (Console.ReadLine() == "ç")
                            Environment.Exit(0);
                        else
                            goto tryingToTypePassword; 
                    }  
            }
        }
     }
 }