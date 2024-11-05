using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class SpellPlacingScript : MonoBehaviour
{
    public GridLayout gridLayout;
    public Tilemap TempTilemap;
    public Tilemap MainTilemap;
    public RessourcesManager RM;

    public bool placementSpell1;
    public bool placementSpell2;
    public bool placementSpell3;
    
    public GameObject boutonSpell1;
    public GameObject boutonSpell2;
    public GameObject boutonSpell3;
    public bool rotationState;
    
    public UnityEvent terraSpellJustHasBeenPlaced = new UnityEvent();
    

    private List<Vector3Int> prevPositions = new List<Vector3Int>();
    
    private SpellForm GetSpellForm(int element)
    {
        return element switch
        {
            1 => SpellForm.Ligne,
            2 => SpellForm.Croix,
            3 => SpellForm.Carre,
            _ => SpellForm.Ligne
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
    
    
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            rotationState = !rotationState;
        }
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (placementSpell1)
        {
            DisplaySpellPreview(mouseWorldPos, RM.spellSlotOne);
            
            if (RM.spellSlotOne.Count == 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    placementSpell1 = false;
                    PlaceSpellTerra(mouseWorldPos, RM.spellSlotOne);
                    ClearPreview();
                    RM.spellSlotOne.Clear();
                    terraSpellJustHasBeenPlaced.Invoke();
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
            
            if (RM.spellSlotTwo.Count == 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    placementSpell2 = false;
                    PlaceSpellTerra(mouseWorldPos, RM.spellSlotTwo);
                    ClearPreview();
                    RM.spellSlotTwo.Clear();
                    terraSpellJustHasBeenPlaced.Invoke();
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
                    placementSpell3 = false;
                    PlaceSpellTerra(mouseWorldPos, RM.spellSlotThree);
                    ClearPreview();
                    RM.spellSlotThree.Clear();
                    terraSpellJustHasBeenPlaced.Invoke();
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

        boutonSpell1.SetActive(RM.spellSlotOne != null && RM.spellSlotOne.Count > 0);
        boutonSpell2.SetActive(RM.spellSlotTwo != null && RM.spellSlotTwo.Count > 0);
        boutonSpell3.SetActive(RM.spellSlotThree != null && RM.spellSlotThree.Count > 0);
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
                positions = GetLineShape(center);
                break;
            case SpellForm.Carre:
                positions = GetSquareShape(center);
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
            center + Vector3Int.up * 2,
            center + Vector3Int.down,
            center + Vector3Int.down * 2,
            center + Vector3Int.left,
            center + Vector3Int.left * 2,
            center + Vector3Int.right,
            center + Vector3Int.right *2,
        };
    }

    private List<Vector3Int> GetLineShape(Vector3Int center)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        if (rotationState == false) // Horizontal line
        {
            positions.Add(center + Vector3Int.left * 4);
            positions.Add(center + Vector3Int.left * 3);
            positions.Add(center + Vector3Int.left * 2);
            positions.Add(center + Vector3Int.left);
            positions.Add(center);
            positions.Add(center + Vector3Int.right);
            positions.Add(center + Vector3Int.right * 2);
            positions.Add(center + Vector3Int.right * 3);
            positions.Add(center + Vector3Int.right * 4);
        }
        else if (rotationState == true) // Vertical line
        {
            positions.Add(center + Vector3Int.down * 4);
            positions.Add(center + Vector3Int.down * 3);
            positions.Add(center + Vector3Int.down * 2);
            positions.Add(center + Vector3Int.down);
            positions.Add(center);
            positions.Add(center + Vector3Int.up);
            positions.Add(center + Vector3Int.up * 2);
            positions.Add(center + Vector3Int.up * 3);
            positions.Add(center + Vector3Int.up * 4);
        }

        return positions;
    }
    

    private List<Vector3Int> GetSquareShape(Vector3Int center)
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                positions.Add(center + new Vector3Int(x, y, 0));
            }
        }
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

        for (int i = 0; i < positions.Count; i++)
        {
            tiles[i] = tileBase;
        }

        MainTilemap.SetTiles(positions.ToArray(), tiles);

        foreach (Vector3Int pos in positions )
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.001f);
            
            foreach (Collider2D objetDetect in colliders)
            {
                if(objetDetect.CompareTag("Tower"))
                {
                    Destroy(objetDetect.transform.parent.gameObject);
                }
            }
        }
        
        
        Building.UpdatePathfinding.Invoke();
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
                    monsterMouvementBehaviours.movementSpell(spellSlot[2]);
                }

            }
        }
        
    }
    public enum SpellForm { Ligne, Croix, Carre }
    public enum SecondaryEffect { SlowAS, Slow, Stun }
    
}
