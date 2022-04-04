using Domain.App;
using System.Text;

namespace DAL.App.EF.DataInit
{
    public static class DataInit
    {
        public static void SeedGamesData(AppDbContext ctx) 
        {
            var game33 = new Game
            {
                Name = "33",
                Language = "en",
                Description = "Mängust võtavad osa vähemalt kaks mängijat. " +
                "Esimene mängija viskab vabaviske joone tagant. " +
                "Kui ta tabas, siis ta saab 3 punkti ja viskab veel kord samast kohast. " +
                "Kui ta viskas mööda, siis järgmine mängija viskab sealt, kus ta püüdis palli. " +
                "Kui järgmine mängija tabas, siis ta läheb vabaviske joone taga, kui ta pani mööda, siis järgmine mängija viskab sealt, kus ta püüdis palli. " +
                "Niimoodi mäng käib niikaua, kui keegi kogub 30 punkti. " +
                "Kui mängijal on 30 punkti, siis iga kord, kui on tema kord viskama, ta valib vabalt koht kolmepunktijoone taga ja viskab sealt. " +
                "Iga tabamine annab 1 punkt. Võidab see, kes esimesena kogub 33 punkti.",
            };

            var gameMinus5 = new Game
            {
                Name = "-5",
                Language = "en",
                Description = "Mängust võtavad osa vähemalt kaks mängijat. " +
                "Esimene mängija teeb vise vabast kohast. " +
                "Kui ta pani korvi sisse, siis algab uus ring, kus igaüks peab sellest samast kohast tabama. " +
                "Kui keegi selle ringi jooksul pani oma vise mööda, siis talle antakse -1 ja järgmine mängija võib visata vabast kohast. " +
                "Kui aga ring on läbi, siis see, kes alustas seda ringi võib jälle visata vabast kohast. " +
                "Kui mängija kogus -5, siis ta lahkub mängust. Võidab see, kes jääb viimasena mängus.",
            };

            var game21 = new Game
            {
                Name = "21",
                Language = "est",
                Description = "Mängust võtavad osa vähemalt kaks mängijat. Esimene mängija viskab vabaviskejoonest. " +
                "Kui ta tabas, siis ta saab 1 punkt. Pärast korvi tabamist mängija alati läheb vabaviskejoonele ja teeb oma järgmine vise sealt. " +
                "Kui ta pani mööda, siis järgmine mängija püüab palli püüdma ja teeb siis oma vise. " +
                "Kui enne viset pall ei kukkunud, siis mängija saab tabamisel 2 punkti. " +
                "Kui enne viset pall põrkes üks kord, siis mängija saab tabamisel 1 punkt. " +
                "Kui mängija paneb mööda, siis järgmine mängija proovib palli püüda ja sooritada oma viset. " +
                "Niimoodi mäng käib niikaua, kui keegi mängijatest kogub 21 punkti. " +
                "Punktide kogumise ajal tuleb arvestada sellega, et kui mängijal oli 10 või 20 punkti, ning ta viskas mööda, siis tal on 0 punkti. ",
            };

            var gameAroundTheWorld = new Game
            {
                Name = "Around the world",
                Language = "est",
                Description = "Mängust võtavad osa vähemalt kaks mängijat ja mängitakse ühel korvpalli väljaku poolel. " +
                "Mängijad püüavad läbida kõik ette määratud kohad. " +
                "Igal mängijal on kolm võimalust teha oma vise." +
                "Vaba – igal mängijal on õigus teha vaba vise igal kohal ilma karistuseta mööda panemise eest. " +
                "Järgmine vise toimub kohalt, kus oli viimane mööda vise." +
                "Võimalus – igal mängijal on õigus teha teine vise igal kohal. " +
                "Kui see vise läheb mööda, siis mängija läheb esimesse ette määratud kohale ja alustab uuesti oma järgmisest visest." +
                "Elu – igal mängijal on õigus teha kolmas vise igal kohal. Kui see vise läheb mööda, siis mängija kaotas ja lahkub mängust." +
                "Esimene mängija alustab esimesest ette määratud kohast ja liigub järgmisele kohale iga sisse pandud korviga. " +
                "Pärast mööda panemist ta peab andma pall järgmisele mängijale või valida vastavalt olukorrale siis kas „Võimalus“ või „Elu“. " +
                "Võidab see mängija, kes esimesena läbis kõik kohad ",
            };

            ctx.Games.Add(game33);
            ctx.Games.Add(gameMinus5);
            ctx.Games.Add(game21);
            ctx.Games.Add(gameAroundTheWorld);
            ctx.SaveChanges();
        }
    }
}
