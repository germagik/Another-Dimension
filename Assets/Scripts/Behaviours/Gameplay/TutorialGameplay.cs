

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class TutorialGameplay : Gameplay
{
    Warrior warrior;
    bool hasMoved = false;
    bool hasAttacked = false;
    bool hasStarted = false;
    bool bossAppeared = false;
    Enemy dummyEnemy;
    public float dummySpawnRange = 20f;
    public float destroyMessageTime = 5f;
    public float bossIncomingMessageTime = 5f;
    public Enemy enemyToSpawn;
    public int spawnCount = 1;
    public float spawnDuration = 40f;
    public float spawnFrequency = 2f;
    private float spawnFrequencyTimer = 0f;
    public float spawnRange = 30f;
    private Tilemap center;
    private Tilemap topLeft;
    private Tilemap top;
    private Tilemap topRight;
    private Tilemap right;
    private Tilemap bottomRight;
    private Tilemap bottom;
    private Tilemap bottomLeft;
    private Tilemap left;
    public Enemy boss;
	public GameObject gamePlayPanel;
	public GameObject gameOverPanel;
	public GameObject victoryText;
	public GameObject defeatText;
	public DangerZone zoneToSpawn;
	public float zoneRange = 5f;
	public int zonesCount = 2;
	public float zoneSpawnFrequency = 2f;
	private float zoneSpawnFrequencyTimer = 0f;
    void Start()
    {
		ShowGamePlayPanel();
        warrior = Warrior.Instance();
        warrior.infoText.text = "Usá las flechas para moverte";
        dummyEnemy = GetComponentInChildren<Enemy>(true);
        center = GetComponentInChildren<Tilemap>();
        center.transform.position = warrior.transform.position;
        Grid grid = GetComponentInChildren<Grid>();
        left = Instantiate(center, center.transform.position + new Vector3(-center.localBounds.size.x, 0), center.transform.rotation, grid.transform);
        right = Instantiate(center, center.transform.position + new Vector3(center.localBounds.size.x, 0), center.transform.rotation, grid.transform);
        top = Instantiate(center, center.transform.position + new Vector3(0, -center.localBounds.size.y), center.transform.rotation, grid.transform);
        bottom = Instantiate(center, center.transform.position + new Vector3(0, center.localBounds.size.y), center.transform.rotation, grid.transform);
        topLeft = Instantiate(center, center.transform.position + new Vector3(-center.localBounds.size.x, center.localBounds.size.y), center.transform.rotation, grid.transform);
        bottomLeft = Instantiate(center, center.transform.position + new Vector3(-center.localBounds.size.x, -center.localBounds.size.y), center.transform.rotation, grid.transform);
        topRight = Instantiate(center, center.transform.position + new Vector3(center.localBounds.size.x, center.localBounds.size.y), center.transform.rotation, grid.transform);
        bottomRight = Instantiate(center, center.transform.position + new Vector3(center.localBounds.size.x, -center.localBounds.size.y), center.transform.rotation, grid.transform);
    }
    void UpdateTiles()
    {
        Tilemap[] allTiles = {center, left, topLeft, top, topRight, right, bottomRight, bottom, bottomLeft};
        float minDistance = (center.transform.position - warrior.transform.position).magnitude;
        Tilemap closestTile = center;
        foreach (Tilemap tile in allTiles)
        {
            float distance = (tile.transform.position - warrior.transform.position).magnitude;
            if(distance < minDistance) {
                closestTile = tile;
            }
        }
        if (closestTile == center)
        {
            return;
        } else {
            center.transform.position = closestTile.transform.position;
            left.transform.position = center.transform.position + new Vector3(-center.localBounds.size.x, 0);
            topLeft.transform.position = center.transform.position + new Vector3(-center.localBounds.size.x, center.localBounds.size.y);
            top.transform.position = center.transform.position + new Vector3(0, -center.localBounds.size.y);
            topRight.transform.position = center.transform.position + new Vector3(center.localBounds.size.x, -center.localBounds.size.y);
            right.transform.position = center.transform.position + new Vector3(center.localBounds.size.x, 0);
            bottomRight.transform.position = center.transform.position + new Vector3(center.localBounds.size.x, center.localBounds.size.y);
            bottom.transform.position = center.transform.position + new Vector3(0, center.localBounds.size.y);
            bottomLeft.transform.position = center.transform.position + new Vector3(-center.localBounds.size.x, -center.localBounds.size.y);
        }
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BackToMenu();
		}
        if (warrior.IsDestroyed())
        {
			ShowGameOverPanel(false);
            return;
        }
        UpdateTiles();
        if (!hasMoved)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (direction.magnitude > 0)
            {
                hasMoved = true;
                warrior.infoText.text = "Presioná A para atacar y S para correr";
                dummyEnemy.transform.position = new Vector3(transform.position.x, transform.position.y + dummySpawnRange);
                dummyEnemy.gameObject.SetActive(true);
            }
            return;
        }
        if (!hasAttacked)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                hasAttacked = true;
                warrior.infoText.text = "Apuntarás automáticamente al enemigo más cercano";
            }
            return;
        }
        if (dummyEnemy.IsDestroyed() && destroyMessageTime > 0)
        {
            destroyMessageTime -= Time.deltaTime;
            warrior.infoText.text = "¡Derrota a todos los enemigos!";
            hasStarted = true;
        }
        else if (destroyMessageTime <= 0 && !warrior.IsDestroyed() && !bossAppeared)
        {
            warrior.infoText.text = "";
        }
        if (hasStarted && spawnDuration > 0 && !warrior.IsDestroyed())
        {
            spawnDuration -= Time.deltaTime;
            spawnFrequencyTimer -= Time.deltaTime;
            if (spawnFrequencyTimer <= 0) {
                spawnFrequencyTimer = spawnFrequency;
                for (int i = 0; i < spawnCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * spawnRange;
                    Instantiate(enemyToSpawn, warrior.transform.position + spawnPosition, enemyToSpawn.transform.rotation, transform);
                }
            }
        }
		if (hasStarted && !warrior.IsDestroyed())
		{
			if(!bossAppeared || (bossAppeared && !boss.IsDestroyed()))
			{
				zoneSpawnFrequencyTimer -= Time.deltaTime;
				if (zoneSpawnFrequencyTimer <= 0)
				{
					zoneSpawnFrequencyTimer = zoneSpawnFrequency;
					for (int i = 0; i < zonesCount; i++)
					{
						Vector3 spawnPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * zoneRange;
						Instantiate(zoneToSpawn, warrior.transform.position + spawnPosition, zoneToSpawn.transform.rotation, transform);
					}
				}
			}
		}
        if (!bossAppeared)
        {
            Enemy[] enemiesLeft = GetComponentsInChildren<Enemy>();
            if (enemiesLeft.Length <= 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * spawnRange;
                boss = Instantiate(boss, warrior.transform.position + spawnPosition, boss.transform.rotation, transform);
                warrior.infoText.text = "¡Ha aparecido un jefe!";
                bossAppeared = true;
            }
        } else {
            if (bossIncomingMessageTime > 0)
            {
                bossIncomingMessageTime -= Time.deltaTime;
            }
            else
            {
                warrior.infoText.text = "";
            }
            if (boss.IsDestroyed()) {

				ShowGameOverPanel(true);
            }
        }
    }

	public void Retry()
	{
		Scenes.ReloadScene();
	}

	public void BackToMenu()
	{
		Scenes.OpenScene(Scenes.MainMenu);
	}

	public void ShowGamePlayPanel()
	{
		gamePlayPanel.SetActive(true);
		gameOverPanel.SetActive(false);
	}

	public void ShowGameOverPanel(bool isVictory)
	{
		gamePlayPanel.SetActive(false);
		gameOverPanel.SetActive(true);
		if (isVictory)
		{
			victoryText.SetActive(true);
			defeatText.SetActive(false);
		}
		else
		{
			victoryText.SetActive(false);
			defeatText.SetActive(true);
		}
	}
}
