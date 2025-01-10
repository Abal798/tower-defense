using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class SpellPlacingScript : MonoBehaviour
{
    public GridLayout gridLayout;
    public Tilemap TempTilemap;
    public Tilemap MainTilemap;
    public RessourcesManager RM;

    public Sprite[] spellSprites;

    public bool placementSpell1;
    public bool placementSpell2;
    public bool placementSpell3;
    
    public GameObject boutonSpell1;
    private bool butonSpell1updated = false;
    public GameObject boutonSpell2;
    private bool butonSpell2updated = false;
    public GameObject boutonSpell3;
    private bool butonSpell3updated = false;
    public int rotationState;
    

    

    private List<Vector3Int> prevPositions = new List<Vector3Int>();
    
    private SpellForm GetSpellForm(int element)
    {
        return element switch
        {
            1 => SpellForm.Croix,
            2 => SpellForm.Ligne,
            3 => SpellForm.Carre,
            _ => SpellForm.Croix
        };
    }

    private TileType GetTileEffect(int element)
    {
        return element switch
        {
            1 => TileType.Fire,
            2 => TileType.Water,
            3 => TileType.Earth,
            _ => TileType.Grass
        };
    }

    private SecondaryEffect GetSecondaryEffect(int element)
    {
        return element switch
        {
            1 => SecondaryEffect.SlowAS,
            2 => SecondaryEffect.Slow,
            3 => SecondaryEffect.Stun,
            _ => SecondaryEffect.SlowAS
        };
    }

    public void Spell1()
    {
        if (RM.spellSlotOne.Count >= 2)
        {
            placementSpell1 = true;
        }
    }
    
    public void Spell2()
    {
        if (RM.spellSlotTwo.Count >= 2)
        {
            placementSpell2 = true;
        }
    }
    
    public void Spell3()
    {
        if (RM.spellSlotThree.Count >= 2)
        {
            placementSpell3 = true;
        }
    }


    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)rotationState++;
        else if (scroll < 0f)rotationState--;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (placementSpell1)
        {
            DisplaySpellPreview(mouseWorldPos, RM.spellSlotOne);
            
            if (RM.spellSlotOne.Count == 2)
            {
                if (Input.GetMouseButtonDown(0))                                                
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        placementSpell1 = false;
                        ClearPreview();
                    }
                    else
                    {
                        placementSpell1 = false;
                        PlaySpellSound(RM.spellSlotOne[1]);
                        PlaceSpellTerra(mouseWorldPos, RM.spellSlotOne);
                        ClearPreview();
                        RM.spellSlotOne.Clear();
                        foreach (var tower in GridBuilding.current.listeTowerCo)
                        {
                            if(tower.Value.gameObject != null)tower.Value.gameObject.GetComponent<ActualizeChild>().AcutalizeChild();
            
                        }

                        EndGameStats.EGS.nombreDeSortsPlaces++;
                    }
                    
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell1 = false;
                    ClearPreview();
                }
            }
            else if (RM.spellSlotOne.Count == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    placementSpell1 = false;
                    PlaceSpellDps(mouseWorldPos, RM.spellSlotOne);
                    ClearPreview();
                    RM.spellSlotOne.Clear();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell1 = false;
                    ClearPreview();
                }
            }
            
        }
        if (placementSpell2)
        {
            DisplaySpellPreview(mouseWorldPos, RM.spellSlotTwo);
            
            if (RM.spellSlotTwo.Count == 2 )
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        placementSpell2 = false;
                        ClearPreview();
                    }
                    else
                    {
                        placementSpell2 = false;
                        PlaySpellSound(RM.spellSlotTwo[1]);
                        PlaceSpellTerra(mouseWorldPos, RM.spellSlotTwo);
                        ClearPreview();
                        RM.spellSlotTwo.Clear();
                        foreach (var tower in GridBuilding.current.listeTowerCo)
                        {
                            if(tower.Value.gameObject != null)tower.Value.gameObject.GetComponent<ActualizeChild>().AcutalizeChild();
            
                        }
                        EndGameStats.EGS.nombreDeSortsPlaces++;
                    }
                    
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell2 = false;
                    ClearPreview();
                }
            }
            else if (RM.spellSlotTwo.Count == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    placementSpell2 = false;
                    PlaceSpellDps(mouseWorldPos, RM.spellSlotTwo);
                    ClearPreview();
                    RM.spellSlotTwo.Clear();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell2 = false;
                    ClearPreview();
                }
            }
            
        }
        if (placementSpell3)
        {
            DisplaySpellPreview(mouseWorldPos, RM.spellSlotThree);
            
            if (RM.spellSlotThree.Count == 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        placementSpell3 = false;
                        ClearPreview();
                    }
                    else
                    {
                        placementSpell3 = false;
                        PlaySpellSound(RM.spellSlotThree[1]);
                        PlaceSpellTerra(mouseWorldPos, RM.spellSlotThree);
                        ClearPreview();
                        RM.spellSlotThree.Clear();
                        foreach (var tower in GridBuilding.current.listeTowerCo)
                        {
                            if(tower.Value.gameObject != null)tower.Value.gameObject.GetComponent<ActualizeChild>().AcutalizeChild();
            
                        }
                        EndGameStats.EGS.nombreDeSortsPlaces++; 
                    }
                    
                    
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell3 = false;
                    ClearPreview();
                }
            }
            else if (RM.spellSlotThree.Count == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    placementSpell3 = false;
                    PlaceSpellDps(mouseWorldPos, RM.spellSlotThree);
                    ClearPreview();
                    RM.spellSlotThree.Clear();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    placementSpell3 = false;
                    ClearPreview();
                }
            }
            
        }

        if (RM.spellSlotOne != null && RM.spellSlotOne.Count > 0 && butonSpell1updated == false)
        {
            butonSpell1updated = true;
            if(RM.spellSlotOne[0] == 1 && RM.spellSlotOne[1] == 1)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[0];
            else if(RM.spellSlotOne[0] == 1 && RM.spellSlotOne[1] == 2)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[1];
            else if(RM.spellSlotOne[0] == 1 && RM.spellSlotOne[1] == 3)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[2];
            else if(RM.spellSlotOne[0] == 2 && RM.spellSlotOne[1] == 1)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[3];
            else if(RM.spellSlotOne[0] == 2 && RM.spellSlotOne[1] == 2)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[4];
            else if(RM.spellSlotOne[0] == 2 && RM.spellSlotOne[1] == 3)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[5];
            else if(RM.spellSlotOne[0] == 3 && RM.spellSlotOne[1] == 1)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[6];
            else if(RM.spellSlotOne[0] == 3 && RM.spellSlotOne[1] == 2)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[7];
            else if(RM.spellSlotOne[0] == 3 && RM.spellSlotOne[1] == 3)boutonSpell1.gameObject.GetComponent<Image>().sprite = spellSprites[8];
            boutonSpell1.SetActive(true);
        }
        else if (RM.spellSlotOne != null && RM.spellSlotOne.Count > 0 == false)
        {
            butonSpell1updated = false;
            boutonSpell1.SetActive(false);
        }
        if (RM.spellSlotTwo != null && RM.spellSlotTwo.Count > 0 && butonSpell2updated == false)
        {
            butonSpell2updated = true;
            if(RM.spellSlotTwo[0] == 1 && RM.spellSlotTwo[1] == 1)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[0];
            else if(RM.spellSlotTwo[0] == 1 && RM.spellSlotTwo[1] == 2)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[1];
            else if(RM.spellSlotTwo[0] == 1 && RM.spellSlotTwo[1] == 3)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[2];
            else if(RM.spellSlotTwo[0] == 2 && RM.spellSlotTwo[1] == 1)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[3];
            else if(RM.spellSlotTwo[0] == 2 && RM.spellSlotTwo[1] == 2)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[4];
            else if(RM.spellSlotTwo[0] == 2 && RM.spellSlotTwo[1] == 3)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[5];
            else if(RM.spellSlotTwo[0] == 3 && RM.spellSlotTwo[1] == 1)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[6];
            else if(RM.spellSlotTwo[0] == 3 && RM.spellSlotTwo[1] == 2)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[7];
            else if(RM.spellSlotTwo[0] == 3 && RM.spellSlotTwo[1] == 3)boutonSpell2.gameObject.GetComponent<Image>().sprite = spellSprites[8];
            boutonSpell2.SetActive(true);
        }
        else if (RM.spellSlotTwo != null && RM.spellSlotTwo.Count > 0 == false)
        {
            butonSpell2updated = false;
            boutonSpell2.SetActive(false);
        }
        if (RM.spellSlotThree != null && RM.spellSlotThree.Count > 0 && butonSpell3updated == false)
        {
            butonSpell3updated = true;
            if(RM.spellSlotThree[0] == 1 && RM.spellSlotThree[1] == 1)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[0];
            else if(RM.spellSlotThree[0] == 1 && RM.spellSlotThree[1] == 2)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[1];
            else if(RM.spellSlotThree[0] == 1 && RM.spellSlotThree[1] == 3)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[2];
            else if(RM.spellSlotThree[0] == 2 && RM.spellSlotThree[1] == 1)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[3];
            else if(RM.spellSlotThree[0] == 2 && RM.spellSlotThree[1] == 2)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[4];
            else if(RM.spellSlotThree[0] == 2 && RM.spellSlotThree[1] == 3)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[5];
            else if(RM.spellSlotThree[0] == 3 && RM.spellSlotThree[1] == 1)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[6];
            else if(RM.spellSlotThree[0] == 3 && RM.spellSlotThree[1] == 2)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[7];
            else if(RM.spellSlotThree[0] == 3 && RM.spellSlotThree[1] == 3)boutonSpell3.gameObject.GetComponent<Image>().sprite = spellSprites[8];
            boutonSpell3.SetActive(true);
        }
        else if (RM.spellSlotThree != null && RM.spellSlotThree.Count > 0 == false)
        {
            butonSpell3updated = false;
            boutonSpell3.SetActive(false);
        }
    }
    
    private void DisplaySpellPreview(Vector3 worldPos, List<int> spellSlot)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(worldPos);
        SpellForm form = GetSpellForm(spellSlot[0]);

        List<Vector3Int> previewPositions  = CalculatePreviewPositions(cellPos, form);
        
        if (previewPositions != prevPositions)
        {
            ClearPreview();
            SetSpellPreview(previewPositions, spellSlot);
            prevPositions = previewPositions;
        }
    }
    
    private List<Vector3Int> CalculatePreviewPositions(Vector3Int center, SpellForm form)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        switch (form)
        {
            case SpellForm.Croix:
                positions = GetCrossShape(center);
                break;
            case SpellForm.Ligne:
                positions = GetZShape(center);
                break;
            case SpellForm.Carre:
                positions = GetThumbShape(center);
                break;
            default:
                positions.Add(center); 
                break;
        }

        return positions;
    }

    private List<Vector3Int> GetCrossShape(Vector3Int center)
    {
        return new List<Vector3Int>
        {
            center,
            center + Vector3Int.up,
            center + Vector3Int.down,
            center + Vector3Int.left,
            center + Vector3Int.right,
        };
    }

    private List<Vector3Int> GetZShape(Vector3Int center)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        if (Mathf.Abs(rotationState) % 2 == 1) // Horizontal line
        {
            positions.Add(center + Vector3Int.left + Vector3Int.down);
            positions.Add(center + Vector3Int.left);
            positions.Add(center);
            positions.Add(center + Vector3Int.right);
            positions.Add(center + Vector3Int.right + Vector3Int.up);
        }
        else if (Mathf.Abs(rotationState) % 2 == 0) // Vertical line
        {
            positions.Add(center + Vector3Int.down + Vector3Int.right);
            positions.Add(center + Vector3Int.down);
            positions.Add(center);
            positions.Add(center + Vector3Int.up);
            positions.Add(center + Vector3Int.up + Vector3Int.left);
        }

        return positions;
    }
    

    private List<Vector3Int> GetThumbShape(Vector3Int center)
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        for (int x = 0; x <= 1; x++)
        {
            for (int y = 0; y <= 1; y++)
            {
                positions.Add(center + new Vector3Int(x, y, 0));
            }
        }
        if (Mathf.Abs(rotationState) % 4 == 0) positions.Add(center + Vector3Int.down + Vector3Int.right);
        if (Mathf.Abs(rotationState) % 4 == 1) positions.Add(center + Vector3Int.left);
        if (Mathf.Abs(rotationState) % 4 == 2) positions.Add(center + Vector3Int.up * 2);
        if (Mathf.Abs(rotationState) % 4 == 3) positions.Add(center + Vector3Int.right * 2 + Vector3Int.up);
        return positions;
    }

    private void SetSpellPreview(List<Vector3Int> positions, List<int> spellSlot)
    {
        TileType tileEffect = GetTileEffect(spellSlot[1]);
        TileBase tileBase = GridBuilding.tileBases[tileEffect];

        TileBase[] tiles = new TileBase[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            tiles[i] = tileBase;
        }

        TempTilemap.SetTiles(positions.ToArray(), tiles);
    }

    private Vector3Int[] GetTilePositions(BoundsInt area)
    {
        Vector3Int[] positions = new Vector3Int[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var pos in area.allPositionsWithin)
        {
            positions[counter++] = pos;
        }

        return positions;
    }

    private void ClearPreview()
    {
        TempTilemap.SetTiles(prevPositions.ToArray(), new TileBase[prevPositions.Count]);
        prevPositions.Clear();
    }
    
    
    private void PlaceSpellTerra(Vector3 worldPos, List<int> spellSlot)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(worldPos);
        SpellForm form = GetSpellForm(spellSlot[0]);
        List<Vector3Int> positions = CalculatePreviewPositions(cellPos, form);

        TileType tileEffect = spellSlot.Count >= 2 ? GetTileEffect(spellSlot[1]) : TileType.Water;

        TileBase tileBase = GridBuilding.tileBases[tileEffect];
        TileBase[] tiles = new TileBase[positions.Count];
        int numberToAdd = 0;
        
        for (int i = 0; i < positions.Count; i++)
        {
            if (GridBuilding.current.MainTilemap.GetTile(positions[i]) != GridBuilding.tileBases[TileType.Base])
            {
                tiles[i] = tileBase;
                numberToAdd += 1;
            }
            else
            {
                tiles[i] = GridBuilding.tileBases[TileType.Base];
            }
        }
        
        foreach (Vector3Int pos in positions )
        {
            RM.UpdateTileNumber(pos, 1, false);
            if (GridBuilding.current.listeTowerCo.ContainsKey(pos))
            {
                Destroy(GridBuilding.current.listeTowerCo[pos]);
            }
        }
        
        
        
        MainTilemap.SetTiles(positions.ToArray(), tiles);
        RM.UpdateTileNumber(cellPos, numberToAdd, true);

        
        
        
        Building.UpdateSurroundings.Invoke();
    }
    
    
    private void PlaceSpellDps(Vector3 worldPos, List<int> spellSlot)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(worldPos);
        SpellForm form = GetSpellForm(spellSlot[0]);
        List<Vector3Int> positions = CalculatePreviewPositions(cellPos, form);
        SecondaryEffect secondary = spellSlot.Count == 3 ? GetSecondaryEffect(spellSlot[2]) : SecondaryEffect.Stun;
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        
        foreach (GameObject monstres in enemies)
        {
            Vector3Int enemyTilePos = gridLayout.WorldToCell(monstres.transform.position);
            
            if (positions.Contains(enemyTilePos))
            {
                MonsterDeathBehaviour monsterDeathBehaviour = monstres.GetComponent<MonsterDeathBehaviour>();
                MonsterMouvementBehaviours monsterMouvementBehaviours= monstres.GetComponent<MonsterMouvementBehaviours>();
                if (monsterDeathBehaviour != null)
                {
                    monsterDeathBehaviour.DamageSpell(spellSlot[1]);
                    //monsterMouvementBehaviours.movementSpell(spellSlot[2]);
                }

            }
        }
        
    }

    private void PlaySpellSound(int spellType)
    {
        if (spellType == 1)
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.fireSpellplacing);
        }
        else if (spellType == 2)
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.waterSpellplacing);
        }
        
        else if (spellType == 3)
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.earthSpellplacing);
        }
    }
    public enum SpellForm { Ligne, Croix, Carre }
    public enum SecondaryEffect { SlowAS, Slow, Stun }
    
}
