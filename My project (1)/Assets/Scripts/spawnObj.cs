using UnityEngine;
//TODO: œ≈–≈–Œ¡»“» ¬—≈ ÔÓ‚Ì≥ÒÚ˛!!!!!!!
namespace Generator
{
    public class spawnObj : MonoBehaviour
    {

        [SerializeField] private GameObject[] Walls;
        [SerializeField] private GameObject[] WallsGun;
        [SerializeField] private GameObject[] Grounds;
        [SerializeField] private GameObject Finish;

        [SerializeField] private BoxCollider CamBox;
        [SerializeField] private GameObject spawnPlayer;
        [SerializeField] private TextAsset levelJSON;


        [SerializeField] private GameObject coin;
        [SerializeField] private GameObject gem;

        [System.Serializable]
        public class Level
        {
            public string[] levelArray;
            public int levelID;
        }

        [System.Serializable]
        public class LevelList
        {
            public Level[] level;
        }

        public LevelList myLevelList = new LevelList();

        string[] test = { };

        private int currentLevel;

        void Start()
        {

            currentLevel = PlayerPrefs.GetInt("currentLevel") - 1;
            Debug.Log(currentLevel + "q" + PlayerPrefs.GetInt("currentLevel"));
            myLevelList = JsonUtility.FromJson<LevelList>(levelJSON.text);

            test = myLevelList.level[currentLevel].levelArray;

            generateGroundAndMore();
            generateHorizontalWall();
            generateVerticalWall();
            spawnCamBox();

        }

        private void generateHorizontalWall()
        {
            int xHorizontalWall = 9;

            for (int i = 0; i < test.GetLength(0); i += 2)
            {
                int zHorizontalWall = 15;
                for (int j = 1; j < test[i].Length; j += 2)
                {
                    switch (test[i][j])
                    {
                        case '#':
                            Instantiate(Walls[0], new Vector3(xHorizontalWall, 0, zHorizontalWall), new Quaternion(0, 0, 0, 0));
                            break;
                        case 'D':
                            Instantiate(WallsGun[0], new Vector3(xHorizontalWall, 0, zHorizontalWall), new Quaternion(0, 0, 0, 0));
                            break;
                        case 'U':
                            Instantiate(WallsGun[0], new Vector3(xHorizontalWall, 0, zHorizontalWall), new Quaternion(0, 45, 0, 0));
                            break;
                    }
                    zHorizontalWall = zHorizontalWall + 18;
                }
                xHorizontalWall = xHorizontalWall + 18;
            }
        }

        private void generateVerticalWall()
        {
            int zVerticalWall = 18;
            for (int i = 1; i < test.GetLength(0); i += 2)
            {
                int xVerticalWall = 6;
                for (int j = 0; j < test[i].Length; j += 2)
                {
                    switch (test[i][j])
                    {
                        case '#':
                            Instantiate(Walls[0], new Vector3(zVerticalWall, 0, xVerticalWall), new Quaternion(0, 45, 0, 45));
                            break;
                        case 'D':
                            Instantiate(WallsGun[0], new Vector3(zVerticalWall, 0, xVerticalWall), new Quaternion(0, -90, 0, 90));
                            break;
                        case 'U':
                            Instantiate(WallsGun[0], new Vector3(zVerticalWall, 0, xVerticalWall), new Quaternion(0, 45, 0, 45));
                            break;
                    }

                    xVerticalWall = xVerticalWall + 18;
                }
                zVerticalWall = zVerticalWall + 18;
            }
        }

        private void generateGroundAndMore()
        {
            //foreach (var g in Grounds)
            //    g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            
            //x100 z103
            int xGround = 18;
            //var pla = spawnPlayer.GetComponent<SelectCharacterOnMainScene>();

            for (int i = 1; i < test.GetLength(0); i += 2)
            {
                int zGround = 15;
                for (int j = 1; j < test[i].Length; j += 2)
                {
                    int chance = Random.Range(0, 100) + 1;
                    switch (test[i][j])
                    {
                        case ' ':
                            if(chance <= 55) { Instantiate(coin, new Vector3(xGround, 3, zGround), Quaternion.identity); }
                            else if(chance > 85) { Instantiate(gem, new Vector3(xGround, 3, zGround), Quaternion.identity); }
                            Instantiate(Grounds[0], new Vector3(xGround, 0, zGround), Quaternion.identity);
                            break;
                        case 'G':
                            Instantiate(gem, new Vector3(xGround, 3, zGround), Quaternion.identity);
                            break;
                        case '|':
                            Instantiate(Grounds[1], new Vector3(xGround, 0, zGround), Quaternion.identity);
                            break;
                        case '-':
                            Instantiate(Grounds[1], new Vector3(xGround, 0, zGround), new Quaternion(0, 45, 0, 45));
                            break;
                        case 's':
                            Instantiate(Grounds[0], new Vector3(xGround, 0, zGround), Quaternion.identity);
                            spawnPlayer.transform.position = new Vector3(xGround, 10, zGround);
                            break;
                        case 'f':
                            Instantiate(Finish, new Vector3(xGround, 5, zGround), Quaternion.identity);
                            Instantiate(Grounds[0], new Vector3(xGround, 0, zGround), Quaternion.identity);
                            break;

                    }
                    zGround = zGround + 18;
                }
                xGround = xGround + 18;
            }
        }

        private void spawnCamBox()
        {
            float getSizeRow = (test.GetLength(0) - 1) / 2;
            float getSizeCol = (test[0].Length - 1) / 2;

            float sizeBoxCollX = (18 * getSizeRow) + 36;
            float sizeBoxCollZ = (18 * getSizeCol) + 36;


            float moveBoxCoolX = 9 + 9f * getSizeRow;
            float moveBoxCoolZ = 6 + 9f * getSizeCol;

            CamBox = CamBox.GetComponent<BoxCollider>();

            CamBox.transform.position = new Vector3(moveBoxCoolX, 0, moveBoxCoolZ);
            CamBox.size = new Vector3(sizeBoxCollX, 1000, sizeBoxCollZ);
        }
    }
}