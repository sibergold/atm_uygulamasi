using System;
 namespace atm_uygulamasi
 {
     public static class Logger{
        static string _rootPath = Directory.GetCurrentDirectory();
        static string _logsFolderPath = "logs";
        static string _EndOftheDayFolderPath = "EOD";
        static string logPath= "";
        static void Update()
        {
            logPath = EOD_Directory() + "\\" + TodayLog()+".txt";
        }
        static string EOD_Directory(){
            string EOD_path = _rootPath+"\\"+_logsFolderPath+"\\"+_EndOftheDayFolderPath;
            Directory.CreateDirectory(EOD_path);
            return EOD_path;
        }
        static string TodayLog(){
            string DayDate = DateTime.Now.Date.ToString("ddMMyyyy");
            string logName = "EOD_Tarih("+DayDate+")";
            return logName;
        }
        public static void LogMaker(string log){
            string CurrentTime = DateTime.Now.ToString("HH : mm : ss");
            Update();
            using (System.IO.StreamWriter editor = new System.IO.StreamWriter(logPath,true)){
            editor.WriteLine(log);
            editor.WriteLine("____________ " + CurrentTime + " ____________________");
            editor.WriteLine();
            }
        }
        public static void  LogShower(){
            string logText = "";
            Update();
            if (File.Exists(logPath)){
            logText = System.IO.File.ReadAllText(logPath);}
            Console.WriteLine(logText);
            Console.WriteLine("Devam etmek için herhangi bir tuşa basınız...");
            Console.WriteLine();
            Console.ReadKey();
        }
     }
 }