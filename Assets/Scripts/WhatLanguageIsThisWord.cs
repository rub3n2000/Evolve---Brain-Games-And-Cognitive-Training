using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatLanguageIsThisWord : MonoBehaviour
{
    int maxRounds = 10;
    int currentRound = 0;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    Text wordText;
    [SerializeField]
    Text button1Text;
    [SerializeField]
    Text button2Text;
    [SerializeField]
    Text button3Text;
    [SerializeField]
    Text button4Text;
    Color originalColor;
    int currentAnswerId;
    string[] languages = new string[11] { "English", "Russian", "Chinese", "Spanish", "German",
        "French", "Norwegian", "Turkish", "Italian", "Arabic", "Hindi" };
    string[] englishWords = new string[187] {"about","after","again","air","all","along","also","an","and","another","any","are","around","as",
"at","away","back","be","because","been","before","below","between","both","but","by","came","can","come","could","day","did",
"different","do","does","dont","down","each","end","even","every","few","find","first","for","found","from","get","give","go","good","great",
"had","has","have","he","help","her","here","him","his","home","house","how","I","if","in","into","is","it","its","just","know","large","last",
"left","like","line","little","long","look","made","make","man","many","may","me","men","might","more","most","Mr","must","my","name","never","new",
"next","no","not","now","number","of","off","old","on","one","only","or","other","our","out","over","own","part","people","place","put","read","right",
"said","same","saw","say","see","she","should","show","small","so","some","something","sound","still","such","take","tell","than","that","the","them",
"then","there","these","they","thing","think","this","those","thought","three","through","time","to","together","too","two","under","up","us","use","very",
"want","water","way","we","well","went","were","what","when","where","which","while","who","why","will","with","word","work","world",
"would","write","year","you","your","was"};
    string[] russianWords = new string[51] {"И", "В(Во)", "He","На", "Я","Он","Что","С(со)","Это","Быть(есть)",
        "А","Весь(Вся, Всё, Все)","Они", "Она","Как","Мы","К(Ко)","У","Вы","Этот(Эта, Это, Эти)",
        "За","Тот(та, то, те)","Но","Ты","По","Из","О(об, обо)","Свой","Так","Один(Одна, Одно)",
        "Вот","Который","Наш","Только","Ешё","От","Такой","Мочь","Говорить","Сказать","Для",
        "Уже","Знать","Да","Какой","Когда","Другой","Первый","Чтобы","Его","Год"};
    string[] chineseWords = new string[101] {"zhōu","nián","jīntiān","míngtiān","zuótiān","rìlì","miǎo","xiǎoshí","fēnzhōng",
"diǎn","zhōngbiǎo","yí ge xiǎo shí","néng","yòng","zuò","qù","lái","xiào","zuò","kàn","yuǎn","xiǎo","hǎo","piàoliang","chǒu","nán",
"jiǎndān","huài","jìn","Hěn gāoxìng jiàn dào nǐ","Nǐhǎo","Zǎoshàng hǎo","Xiàwǔ hǎo","Wǎnshàng hǎo","Wǎn'ān","Nǐhǎo ma？","Xièxiè","Bù","Hǎochī!",
"Wǒ shì ...","Zàijiàn","Shì de","xīngqī yī","xīngqī èr","xīngqī sān","xīngqī sì","xīngqī wǔ","xīngqī liù","xīngqītiān","wǔ yuè",
"yī yuè","èr yuè","sān yuè","sì yuè","liù yuè","qī yuè","bā yuè","jiǔ yuè","shí yuè","shíyī yuè","shíèr yuè","líng","yī","èr","sān","wǔ",
"liù","qī","bā","jiǔ","shí","kāfēi","píjiǔ","chá","pútaojiǔ","shuǐ","niúròu","zhūròu","jīròu","gāoyáng ròu","yú","jiǎo","tuǐ","tóu","shǒubì",
"shǒu","shǒuzhǐ","shēntǐ","wèi","bèi","xiōng","hùshì","yuángōng","jǐngchá","chúshī","gōngchéngshī","yīshēng","jīnglǐ","lǎoshī","chéngxù shèjìyuán",
"tuīxiāoyuán"
};
    string[] spanishWords = new string[58] {"el / la","de","que","y","a","por","un","su ","para","como",
"estar","tener","le","lo","todo","pero","más","hacer","o","poder","decir","este","ir","otro","ese","la","si",
"me","ya","ver","porque","dar","cuando","él ","muy","sin","vez","mucho","saber","qué","sobre","mi","alguno","mismo",
"también","hasta","querer","año","dos","desde","primero","illegar","pasar","ella","sí","bien","poco","uno"
 };
    string[] germanWords = new string[44] {"der / die / das","und ","sein","ich","zu","haben","werden","sie","von",
"nicht","mit","sich","auch","auf","für","so","dass","können","dies","als","ihr","wie","bei","oder","wir","aber","dann",
"sein","noch","nach","aus","wenn","nur","müssen","sagen","um","über","machen","kein","jahr","mein","schon","vor","durch"
};
    string[] frenchWords = new string[30] {"depuis","comprendre","bon","lequel","venir","dernier","rendre","trouver",
"après","alors","monsieur","demander","jour","monde","notre","quelque","falloir","savoir","aucun","déjà","nouveau",
"deux","même","prendre","leur","mettre","tout","pouvoir","que","être"
 };
    string[] norwegianWords = new string[49] { "uke","år","i dag","i morgen","i går","kalender","sekund",
"minutt","klokken","klokke","kunne","bruke","gjøre","gå","komme","lage","se","langt","liten","god","vakker","stygg",
"vanskelig","enkel","dårlig","nære","hyggelig å møte deg","god morgen","hallo","god ettermiddag","god kveld","god natt",
"hvordan går det med deg?","takk","nei","delig","jeg heter","farvel","mandag","tirsdag","onsdag","torsdag","fredag",
"lørdag","søndag","mai","januar","februar","mars"};
    string[] turkishWords = new string[57] {"hafta","yıl","bugün","yarın","dün","takvim","saniye","saat","dakika","Yapabilmek","kullanmak",
"Yapmak","gitmek","Gelmek","gülmek","yapmak","görmek","uzak","küçük","iyi","güzel","çirkin","zor","kolay","kötü","yakın","Tanıştığımıza memnun oldum",
"Günaydın","Merhaba","İyi günler","İyi akşamlar","İyi geceler","Nasılsın?","Teşekkür ederim","Hayır","Leziz","Güle güle","Evet","Pazartesi","Salı",
"Çarşamba","Perşembe","Cuma","Cumartesi","Pazar","Mayıs","Ocak","Şubat","Mart","Nisan","Haziran","temmuz","Ağustos","Eylül","Ekim","Kasım","Aralık"};
    string[] italianWords = new string[42] {"settimana","anno","oggi","domani","ieri","calendario","secondo","ora","minuto",
"in punto","orologio","potere","usare","andare","venire","preparare","vedere","lontano","piccolo","buono","bello","brutto",
"difficile","facile","cattivo","vicino","Piacere di conoscerti","Ciao","Buongiorno","Buon pomeriggio","Buonasera","Buonanotte",
"Come stai?","Grazie!","Arrivederci","lunedì","martedì","mercoledì","giovedì","venerdì","sabato","domenica"};
    string[] arabianWords = new string[42] {"ṣaġīr","ǧayyid","ǧamīl","qabīḥ","biǧiwār","marḥaban","ṣabāḥu al-ḫaīr","masaāʾu al-ḫaīr","tuṣbiḥ ʿlā ḫayr",
"kayfa ḥāluk?","šukran","mʿ al-slāmh","ʾal-ʾiṯnain","ʾal-ṯulāṯāʾ","ʾl-ʾarbiʿāʾ","ʾal-ḫamīs","ʾaǧ-ǧumʿah","ʾas-sabt","al-ʾaḥad","māyū","yanāyir",
"fibrāyir","māris","ʾibrīl","yūnyū","yūlyū","ʾuġusṭus","sibtambir","ʾuktūbar","nūfambir","dīsambir","ṣifr","wāḥid","ʾiṯnān","ṯalāṯah",
"ʾarbaʿah","ḫamsah","sittah","sabʿah","ṯamāniyah","tisʿah","ʿašrah"};
    string[] hindiWords = new string[30] {"hafTaa","SaaL","aaj","kal","ek ghante","SakNaa","upayog karNaa","karNaa","jaaNaa","aaNaa",
"haSNaa","baNaaNaa","dekhNaa","Duur","chotaa","acchaa","SuNDar","baDSuuraT","kathiN","aasaan","buraa","aap Se miLkar khusii huii",
"NamaSTe","SuprabhaaT","subh SaNDHyaa","subh raaTrii","aap kaiSe hain?","DHaNyavaaD!","Nahiin","swaaDist"};
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endscreenText;
    List<string[]> stringArrays;
    List<int> scores;
    bool canAnswer = true;
    AntonymsSfxManager antonymsSfxManager;
    // Start is called before the first frame update
    void Start()
    {
        stringArrays = new List<string[]>();
        stringArrays.Add(englishWords); stringArrays.Add(russianWords); stringArrays.Add(chineseWords); stringArrays.Add(spanishWords);
        stringArrays.Add(germanWords); stringArrays.Add(frenchWords); stringArrays.Add(norwegianWords); stringArrays.Add(turkishWords);
        stringArrays.Add(italianWords); stringArrays.Add(arabianWords); stringArrays.Add(hindiWords);
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        endScreen.SetActive(false);
        game.SetActive(true);
        originalColor = button1Text.color;
        scores = new List<int>();
        SetupRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Guess(int id)
    {
        if (canAnswer)
        {
            canAnswer = false;
            switch (currentAnswerId)
            {
                case 0:
                    button1Text.color = Color.green; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 1:
                    button1Text.color = Color.red; button2Text.color = Color.green; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 2:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.green; button4Text.color = Color.red;
                    break;
                case 3:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.green;
                    break;
            }

            if (id == currentAnswerId)
            {
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                antonymsSfxManager.PlayAudio(true);
                scoreKeeper.languagePoints += 100;
                scores.Add(100);
                if (scoreKeeper.languagePoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.languageLevel + 1])
                {
                    scoreKeeper.languageLevel++;
                }
                saveLoader.SaveGameData();
            }
            else { scores.Add(0); antonymsSfxManager.PlayAudio(false); }
            Invoke("StartNewRound", 1);
        }
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        endScreen.SetActive(true);
        game.SetActive(false);
        endscreenText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == 0)
            {
                endscreenText.text += "Round " + i + " | Wrong | 0 points" + "\n";
            }
            else
            {
                endscreenText.text += "Round " + i + " | Correct | " + scores[i] + " points" + "\n";
            }
        }
        endscreenText.text += "\n" + " Well Done, Keep Improving!";
    }

    void StartNewRound()
    {
        if (currentRound >= 10)
        {
            EndGame();
        }
        else
        {
            button1Text.color = originalColor; button2Text.color = originalColor;
            button3Text.color = originalColor; button4Text.color = originalColor;
            currentRound++;
            canAnswer = true;
            SetupRound();
        }
    }

    void SetupRound()
    {
        int languageIndex = Random.Range(0, stringArrays.Count);
        wordText.text = stringArrays[languageIndex][Random.Range(0, stringArrays[languageIndex].Length)];
        currentAnswerId = Random.Range(0, 4);
        switch(currentAnswerId)
        {
            case 0:
                button1Text.text = languages[languageIndex];
                int randomIndex11 = Random.Range(0, languages.Length);
                while(randomIndex11 == languageIndex)
                {
                    randomIndex11 = Random.Range(0, languages.Length);
                }
                button2Text.text = languages[randomIndex11];
                int randomIndex12 = Random.Range(0, languages.Length);
                while (randomIndex12 == randomIndex11 || randomIndex12 == languageIndex)
                {
                    randomIndex12 = Random.Range(0, languages.Length);
                }
                button3Text.text = languages[randomIndex12];
                int randomIndex13 = Random.Range(0, languages.Length);
                while (randomIndex13 == languageIndex || randomIndex13 == randomIndex12 || randomIndex13 == randomIndex11)
                {
                    randomIndex13 = Random.Range(0, languages.Length);
                }
                button4Text.text = languages[randomIndex13];
                break;
            case 1:
                button2Text.text = languages[languageIndex];
                int randomIndex21 = Random.Range(0, languages.Length);
                while (randomIndex21 == languageIndex)
                {
                    randomIndex21 = Random.Range(0, languages.Length);
                }
                button1Text.text = languages[randomIndex21];
                int randomIndex22 = Random.Range(0, languages.Length);
                while (randomIndex22 == languageIndex || randomIndex22 == randomIndex21)
                {
                    randomIndex22 = Random.Range(0, languages.Length);
                }
                button3Text.text = languages[randomIndex22];
                int randomIndex23 = Random.Range(0, languages.Length);
                while (randomIndex23 == languageIndex || randomIndex23 == randomIndex22 || randomIndex23 == randomIndex21)
                {
                    randomIndex23 = Random.Range(0, languages.Length);
                }
                button4Text.text = languages[randomIndex23];
                break;
            case 2:
                button3Text.text = languages[languageIndex];
                int randomIndex31 = Random.Range(0, languages.Length);
                while (randomIndex31 == languageIndex)
                {
                    randomIndex31 = Random.Range(0, languages.Length);
                }
                button1Text.text = languages[randomIndex31];
                int randomIndex32 = Random.Range(0, languages.Length);
                while (randomIndex32 == languageIndex || randomIndex32 == randomIndex31)
                {
                    randomIndex32 = Random.Range(0, languages.Length);
                }
                button2Text.text = languages[randomIndex32];
                int randomIndex33 = Random.Range(0, languages.Length);
                while (randomIndex33 == languageIndex || randomIndex33 == randomIndex32 || randomIndex33 == randomIndex31)
                {
                    randomIndex33 = Random.Range(0, languages.Length);
                }
                button4Text.text = languages[randomIndex33];
                break;
            case 3:
                button4Text.text = languages[languageIndex];
                int randomIndex41 = Random.Range(0, languages.Length);
                while (randomIndex41 == languageIndex)
                {
                    randomIndex41 = Random.Range(0, languages.Length);
                }
                button1Text.text = languages[randomIndex41];
                int randomIndex42 = Random.Range(0, languages.Length);
                while (randomIndex42 == languageIndex || randomIndex42 == randomIndex41)
                {
                    randomIndex42 = Random.Range(0, languages.Length);
                }
                button3Text.text = languages[randomIndex42];
                int randomIndex43 = Random.Range(0, languages.Length);
                while (randomIndex43 == languageIndex || randomIndex43 == randomIndex42 || randomIndex43 == randomIndex41)
                {
                    randomIndex43 = Random.Range(0, languages.Length);
                }
                button2Text.text = languages[randomIndex43];
                break;
        }

    }
}
