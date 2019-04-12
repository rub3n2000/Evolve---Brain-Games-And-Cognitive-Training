    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortStory : MonoBehaviour
{
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    GameObject gameContainer;
    [SerializeField]
    GameObject endContainer;
    [SerializeField]
    GameObject part1Container;
    [SerializeField]
    GameObject part2Container;
    [SerializeField]
    Text timerText;
    [SerializeField]
    Text storyText;
    [SerializeField]
    Text questionText;
    [SerializeField]
    Text answerText;
    [SerializeField]
    Text endText;
    float timer;
    List<int> scores;

    string[] names = new string[1000] {"Liam","Noah","William","James", "Logan", "Benjamin", "Mason","Elijah","Oliver","Jacob","Lucas","Michael","Alexander","Ethan","Daniel","Matthew",
                                                "Aiden", "Henry","Joseph","Jackson","Samuel","Sebastian","David","Carter","Wyatt","Jayden","John","Owen","Dylan",
                                                "Luke", "Gabriel","Anthony","Isaac","Grayson","Jack","Julian","Levi","Christopher","Joshua","Andrew","Lincoln","Mateo","Ryan","Jaxon","Nathan",
                                                "Aaron", "Isaiah","Thomas","Charles","Caleb","Josiah","Christian","Hunter","Eli","Jonathan","Connor","Landon","Adrian","Asher","Cameron","Leo",
                                                "Theodore","Jeremiah","Hudson","Robert","Easton","Nolan","Nicholas","Ezra","Colton","Angel","Brayden","Jordan","Dominic","Austin","Ian","Adam",
                                                "Elias","Jaxson","Greyson","Jose","Ezekiel","Carson","Evan","Maverick","Bryson","Jace","Cooper","Xavier","Parker","Roman","Jason","Santiago","Chase","Sawyer","Gavin",
                                                "Leonardo", "Kayden", "Ayden", "Jameson", "Kevin", "Bentley", "Zachary", "Everett", "Axel", "Tyler", "Micah", "Vincent", "Weston", "Miles", "Wesley", "Nathaniel", "Harrison",
                                                "Brandon","Cole","Declan","Luis","Braxton","Damian","Silas","Tristan","Ryder","Bennett","George","Emmett","Justin","Kai","Max","Diego",
                                                "Luca","Ryker","Carlos","Maxwell","Kingston","Ivan","Maddox","Juan","Ashton","Jayce","Rowan","Kaiden","Giovanni","Eric",
                                                "Jesus","Calvin","Abel","King","Camden","Amir","Blake","Alex","Brody","Malachi","Emmanuel","Jonah","Beau","Jude","Antonio","Alan","Elliott",
                                                "Elliot","Waylon","Xander","Timothy","Victor","Bryce","Finn","Brantley","Edward","Abraham","Patrick","Grant","Karter","Hayden","Richard","Miguel","Joel",
                                                "Gael","Tucker","Rhett","Avery","Steven","Graham","Kaleb","Jasper","Jesse","Matteo","Dean","Zayden","Preston","August","Oscar","Jeremy",
                                                "Alejandro","Marcus","Dawson","Lorenzo","Messiah","Zion","Maximus","River","Zane","Mark","Brooks","Nicolas","Paxton","Judah","Emiliano","Kaden",
                                                "Bryan","Kyle","Myles","Peter","Charlie","Kyrie","Thiago","Brian","Kenneth","Andres","Lukas","Aidan","Jax","Caden","Milo","Paul","Beckett",
                                                "Brady","Colin","Omar","Bradley","Javier","Knox","Jaden","Barrett","Israel","Matias","Jorge","Zander","Derek",
                                                "Josue","Cayden","Holden","Griffin","Arthur","Leon","Felix","Remington","Jake","Killian","Clayton","Sean","Adriel","Riley","Archer",
                                                "Legend","Erick","Enzo","Corbin","Francisco","Dallas","Emilio","Gunner","Simon","Andre","Walter","Damien","Chance","Phoenix","Colt","Tanner","Stephen",
                                                "Kameron","Tobias","Manuel","Amari","Emerson","Louis","Cody","Finley","Iker","Martin","Rafael","Nash","Beckham","Cash","Karson","Rylan","Reid","Theo","Ace",
                                                "Eduardo","Spencer","Raymond","Maximiliano","Anderson","Ronan","Lane","Cristian","Titus","Travis","Jett","Ricardo","Bodhi","Gideon","Jaiden","Fernando","Mario",
                                                "Conor","Keegan","Ali","Cesar","Ellis","Jayceon","Walker","Cohen","Arlo","Hector","Dante","Kyler","Garrett","Donovan","Seth","Jeffrey","Tyson","Jase","Desmond","Caiden",
                                                "Gage","Atlas","Major","Devin","Edwin","Angelo","Orion","Conner","Julius","Marco","Jensen","Daxton","Peyton","Zayn","Collin",
                                                "Jaylen","Dakota","Prince","Johnny","Kayson","Cruz","Hendrix","Atticus","Troy","Kane","Edgar","Sergio","Kash","Marshall",
                                                "Johnathan","Romeo","Shane","Warren","Joaquin","Wade","Leonel","Trevor","Dominick","Muhammad","Erik","Odin","Quinn","Jaxton","Dalton","Nehemiah","Frank","Grady",
                                                "Gregory","Andy","Solomon","Malik","Rory","Clark","Reed","Harvey","Zayne","Jay","Jared","Noel","Shawn","Fabian","Ibrahim","Adonis","Ismael",
                                                "Pedro","Leland","Malakai","Malcolm","Alexis","Kason","Porter","Sullivan","Raiden","Allen","Ari","Russell","Princeton","Winston","Kendrick","Roberto",
                                                "Lennox","Hayes","Finnegan","Nasir","Kade","Nico","Emanuel","Landen","Moises","Ruben","Hugo","Abram","Adan","Khalil","Zaiden","Augustus","Marcos","Philip","Phillip","Cyrus",
                                                "Esteban","Braylen","Albert","Bruce","Kamden","Lawson","Jamison","Sterling","Damon","Gunnar","Kyson","Luka","Franklin","Ezequiel","Pablo","Derrick",
                                                "Zachariah","Cade","Jonas","Dexter","Kolton","Remy","Hank","Tate","Trenton","Kian","Drew","Mohamed","Dax","Rocco","Bowen","Mathias","Ronald","Francis","Matthias","Milan",
                                                "Maximilian","Royce","Skyler","Corey","Kasen","Drake","Gerardo","Jayson","Sage","Braylon","Benson","Moses","Alijah","Rhys","Otto",
                                                "Oakley","Armando","Jaime","Nixon","Saul","Scott","Brycen","Ariel","Enrique","Donald","Chandler","Asa","Eden","Davis",
                                                "Keith","Frederick","Rowen","Lawrence","Leonidas","Aden","Julio","Darius","Johan","Deacon","Cason","Danny","Nikolai","Taylor","Alec","Royal","Armani",
                                                "Kieran","Luciano","Omari","Rodrigo","Arjun","Ahmed","Brendan","Cullen","Raul","Raphael","Ronin","Brock","Pierce","Alonzo","Casey","Dillon","Uriel","Dustin","Gianni","Roland","Landyn","Kobe",
                                                "Dorian","Emmitt","Ryland","Apollo","Aarav","Roy","Duke","Quentin","Sam","Lewis","Tony","Uriah","Dennis","Moshe","Isaias","Braden","Quinton","Cannon","Ayaan","Mathew","Kellan","Niko",
                                                "Edison","Izaiah","Jerry","Gustavo","Jamari","Marvin","Mauricio","Ahmad","Mohammad","Justice","Trey","Elian","Mohammed","Sincere","Yusuf","Arturo","Callen","Rayan","Keaton","Wilder",
                                                "Mekhi","Memphis","Cayson","Conrad","Kaison","Kyree","Soren","Colby","Bryant","Lucian","Alfredo","Cassius","Marcelo","Nikolas","Brennan","Darren","Jasiah",
                                                "Jimmy","Lionel","Reece","Ty","Chris","Forrest","Korbin","Tatum","Jalen","Santino","Case","Leonard","Alvin","Issac","Bo","Quincy","Mack",
                                                "Samson","Rex","Alberto","Callum","Curtis","Hezekiah","Finnley","Briggs","Kamari","Zeke","Raylan","Neil","Titan","Julien","Kellen","Devon",
                                                "Kylan","Roger","Axton","Carl","Douglas","Larry","Crosby","Fletcher","Makai","Nelson","Hamza","Lance","Alden","Gary","Wilson","Alessandro","Ares","Kashton",
                                                "Bruno","Jakob","Stetson","Zain","Cairo","Nathanael","Byron","Harry","Harley","Mitchell","Maurice","Orlando","Kingsley","Kaysen",
                                                "Sylas","Trent","Ramon","Boston","Lucca","Noe","Jagger","Reyansh","Vihaan","Randy","Thaddeus","Lennon","Kannon","Kohen","Tristen",
                                                "Valentino","Maxton","Salvador","Abdiel","Langston","Rohan","Kristopher","Yosef","Rayden","Lee","Callan","Tripp","Deandre","Joe","Morgan","Dariel","Colten",
                                                "Reese","Jedidiah","Ricky","Bronson","Terry","Eddie","Jefferson","Lachlan","Layne","Clay","Madden","Jamir","Tomas","Kareem","Stanley","Brayan","Amos","Kase","Kristian",
                                                "Clyde","Ernesto","Tommy","Casen","Ford","Crew","Braydon","Brecken","Hassan","Axl","Boone","Leandro","Samir","Jaziel","Magnus","Abdullah",
                                                "Yousef","Branson","Jadiel","Jaxen","Layton","Franco","Ben","Grey","Kelvin","Chaim","Demetrius","Blaine","Ridge","Colson","Melvin","Anakin","Aryan",
                                                "Lochlan","Jon","Canaan","Dash","Zechariah","Alonso","Otis","Zaire","Marcel","Brett","Stefan","Aldo","Jeffery","Baylor","Talon","Dominik","Flynn","Carmelo",
                                                "Dane","Jamal","Kole","Enoch","Graysen","Kye","Vicente","Fisher","Ray","Fox","Jamie","Rey","Zaid","Allan","Emery","Gannon","Joziah","Rodney","Juelz","Sonny","Terrance","Zyaire",
                                                "Augustine","Cory","Felipe","Aron","Jacoby","Harlan","Marc","Bobby","Joey","Anson","Huxley","Marlon","Anders",
                                                "Guillermo","Payton","Castiel","Damari","Shepherd","Azariah","Harold","Harper","Henrik","Houston","Kairo","Willie","Elisha","Ameer","Emory",
                                                "Skylar","Sutton","Alfonso","Brentley","Toby","Blaze","Eugene","Shiloh","Wayne","Darian","Gordon","London","Bodie",
                                                "Jordy","Jermaine","Denver","Gerald","Merrick","Musa","Vincenzo","Kody","Yahir","Brodie","Trace","Darwin","Tadeo",
                                                "Bentlee","Billy","Hugh","Reginald","Vance","Westin","Cain","Arian","Dayton","Javion","Terrence","Brysen","Jaxxon","Thatcher","Landry",
                                                "Rene","Westley","Miller","Alvaro","Cristiano","Eliseo","Ephraim","Adrien","Jerome","Khalid","Aydin","Mayson","Alfred",
                                                "Duncan","Junior","Kendall","Zavier","Koda","Maison","Caspian","Maxim","Kace","Zackary","Rudy","Coleman","Keagan","Kolten","Maximo","Dario",
                                                "Davion","Kalel","Briar","Jairo","Misael","Rogelio","Terrell","Heath","Micheal","Wesson","Aaden","Brixton","Draven","Xzavier","Darrell",
                                                "Keanu","Ronnie","Konnor","Will","Dangelo","Frankie","Kamryn","Salvatore","Santana","Shaun","Coen","Leighton","Mustafa","Reuben","Ayan","Blaise",
                                                "Dimitri","Keenan","Van","Achilles","Channing","Ishaan","Wells","Benton","Lamar","Nova","Yahya","Dilan","Gibson","Camdyn","Ulises","Alexzander","Valentin",
                                                "Shepard","Alistair","Eason","Kaiser","Leroy","Zayd","Camilo","Markus","Foster","Davian","Dwayne","Jabari","Judson",
                                                "Koa","Yehuda","Lyric","Tristian","Agustin","Bridger","Vivaan","Brayson","Emmet","Marley","Mike","Nickolas","Kenny","Leif","Bjorn","Ignacio","Rocky","Chad",
                                                "Gatlin","Greysen","Kyng","Randall","Reign","Vaughn","Jessie","Louie","Shmuel","Zahir","Ernest","Javon","Khari","Reagan","Avi","Ira","Ledger","Simeon",
                                                "Yadiel","Maddux","Seamus","Jad","Jeremias","Kylen","Rashad","Santos","Cedric","Craig","Dominique","Gianluca","Jovanni","Bishop","Brenden","Anton","Camron","Giancarlo",
                                                "Lyle","Alaric","Decker","Eliezer","Ramiro","Yisroel","Howard","Jaxx",
    };
    [SerializeField]
    string[] jobs;
    [SerializeField]
    string[] schools;
    [SerializeField]
    string[] hairColors;
    [SerializeField]
    string[] eyeColors;
    [SerializeField]
    float[] heights;
    [SerializeField]
    string[] petTypes;
    [SerializeField]
    Image backGround;

    Color originalColor;

    string personName;
    string job;
    string school;
    string hairColor;
    string eyeColor;
    float height;
    int amountOfFriends;
    string bestFriendName;
    List<string> friendNames;
    string petType;
    string petName;
    int petAge;
    string correctAnswer = "";
    [SerializeField]
    int timeAllowedToRead;

    int currentQuestion = 1;
    int maxQuestions = 6;

    List<int> questionIndices;
    int question1Index;
    int question2Index;
    int question3Index;
    int question4Index;
    int question5Index;

    [SerializeField]
    Color red;
    [SerializeField]
    Color green;
    bool hasNotTriggered = true;

    [SerializeField]
    PotentialQuestions[] potentialQuestions;


    // Start is called before the first frame update
    void Start()
    {
        scores = new List<int>();
        questionIndices = new List<int>();
        friendNames = new List<string>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endContainer.SetActive(false);
        gameContainer.SetActive(true);
        originalColor = backGround.color;
        SetupGame();
        timeAllowedToRead -= scoreKeeper.memoryLevel;
        if(timeAllowedToRead < 10)
        {
            timeAllowedToRead = 10;
        }
        timer = timeAllowedToRead;
          
    }

    void SetupGame()
    {
        part2Container.SetActive(false);
        part1Container.SetActive(true);
        question1Index = Random.Range(0, 9);
        question2Index = Random.Range(0, 9);
        question3Index = Random.Range(0, 9);
        question4Index = Random.Range(0, 9);
        question5Index = Random.Range(0, 9);
        while(question2Index == question1Index)
        {
            question2Index = Random.Range(0, 9);
        }
        while(question3Index == question2Index || question3Index == question1Index)
        {
            question3Index = Random.Range(0, 9);
        }
        while (question4Index == question2Index || question4Index == question1Index || question4Index == question3Index)
        {
            question4Index = Random.Range(0, 9);
        }
        while (question5Index == question2Index || question5Index == question1Index || question5Index == question3Index || question5Index == question4Index)
        {
            question5Index = Random.Range(0, 9);
        }
        questionIndices.Add(question1Index);
        questionIndices.Add(question2Index);
        questionIndices.Add(question3Index);
        questionIndices.Add(question4Index);
        questionIndices.Add(question5Index);
        personName = names[Random.Range(0, names.Length - 1)];
        job = jobs[Random.Range(0, jobs.Length - 1)];
        school = schools[Random.Range(0, schools.Length - 1)];
        hairColor = hairColors[Random.Range(0, hairColors.Length - 1)];
        eyeColor = eyeColors[Random.Range(0, eyeColors.Length - 1)];
        height = heights[Random.Range(0, heights.Length - 1)];
        amountOfFriends = Random.Range(2, 6);
        bestFriendName = names[Random.Range(0, names.Length - 1)];
        for(int i = 0; i < amountOfFriends; i++)
        {
            friendNames.Add(names[Random.Range(0, names.Length - 1)]);
        }
        amountOfFriends++;
        petType = petTypes[Random.Range(0, petTypes.Length - 1)];
        petName = names[Random.Range(0, names.Length - 1)];
        petAge = Random.Range(1, 13);
        storyText.text = personName + " was a " + job + " and went to " + school + ". His hair was " + hairColor + " and his eyes " + eyeColor + ". \n ";
        storyText.text += "His best friend was " + bestFriendName + " and the other friends were called ";
        for (int i = 0; i < friendNames.Count - 1; i++)
        {
            if (i != friendNames.Count - 2)
            {
                storyText.text += friendNames[i] + ", ";
            }
            else
            {
                storyText.text += friendNames[i] + " and " + friendNames[i + 1] + ". \n";
            }
        }
        storyText.text += " He had a " + petType + " called " + petName + ".";
    }

    void SetupQuestion()
    {
        backGround.color = originalColor;
        if(currentQuestion >= maxQuestions)
        {
            EndGame();
        }
        currentQuestion++;
        part2Container.SetActive(true);
        part1Container.SetActive(false);
        int questionIndex;
        switch(currentQuestion)
        {
            case 1: questionIndex = question1Index;break;
            case 2: questionIndex = question2Index; break;
            case 3: questionIndex = question3Index; break;
            case 4: questionIndex = question4Index; break;
            default: questionIndex = question5Index; break;
        }
        questionText.text = potentialQuestions[questionIndex].question;
        switch(questionIndex)
        {
            case 0: correctAnswer = personName.ToUpper(); break;
            case 1: correctAnswer = job.ToUpper(); break;
            case 2: correctAnswer = school.ToUpper(); break;
            case 3: correctAnswer = hairColor.ToUpper(); break;
            case 4: correctAnswer = eyeColor.ToUpper(); break;
            case 5: correctAnswer = height.ToString().ToUpper(); break;
            case 6: correctAnswer = amountOfFriends.ToString().ToUpper(); break;
            case 7: correctAnswer = bestFriendName.ToUpper(); break;
            case 8: correctAnswer = friendNames[1].ToUpper(); break;
            case 9: correctAnswer = petType.ToUpper(); break;
            case 10: correctAnswer = petName.ToUpper(); break;
            case 11: correctAnswer = petAge.ToString().ToUpper(); break;
        }

    }

    void EndGame()
    {
        endContainer.SetActive(true);
        gameContainer.SetActive(false);
        endText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == 0)
            {
                endText.text += "Round " + (i + 1).ToString() + " | Wrong |" + "\n";
            }
            else
            {
                endText.text += "Round " + (i + 1).ToString() + " | Correct | " + scores[i] + " points" + "\n";
            }
        }
        endText.text += "\n" + " Well Done, Keep Improving!";
    }

    public void Guess()
    {
        if(answerText.text.ToUpper() == correctAnswer)
        {
                scoreKeeper.memoryPoints += 100;
                scores.Add(100);
                if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                {
                    scoreKeeper.memoryLevel++;
                }
            saveLoader.SaveGameData(); CorrectAnswer();
        }
        else { scores.Add(0); WrongAnswer(); }
        answerText.text = "";
    }

    void CorrectAnswer()
    {
        part2Container.SetActive(false);
        backGround.color = green;
        Invoke("SetupQuestion", 1f);
    }

    void WrongAnswer()
    {
        part2Container.SetActive(false);
        backGround.color = red;
        Invoke("SetupQuestion", 1f);
    }

    public void EnterLetter(string letter)
    {
        answerText.text += letter.ToUpper();
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("0.0");
        
        if(timer<0 && hasNotTriggered)
        {
            SetupQuestion();
            hasNotTriggered = false;
            
            
        }
        Debug.Log(currentQuestion);
    }
    [System.Serializable]
    struct PotentialQuestions
    {
        public int questionIndex;
        public string question;
    }
}
