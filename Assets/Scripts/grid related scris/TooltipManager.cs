using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TooltipManager : MonoBehaviour
{
    public float tooltipDelay = 0.5f;
    private Vector3 lastMousePosition;
    private float timer;
    private bool tooltipActive;
    public Tilemap mainTilemap;
    void Start()
    {
        lastMousePosition = Input.mousePosition; 
        timer = 0f;                          
    }
    
    void Update()
    {
        
        if (Input.mousePosition != lastMousePosition)
        {
            timer = 0f;                      
            lastMousePosition = Input.mousePosition; 
            
            TooltipExtented.HideTooltip_Static();
            TooltipComplet.HideTooltip_Static();
        }
        else
        {
            timer += Time.deltaTime;          
        }

        
        if (timer >= tooltipDelay)
        {
           
            if (GridBuilding.current.tempEmpty())
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                switch (mainTilemap.GetTile(Vector3Int.FloorToInt(GridBuilding.current.gridLayout.LocalToCell(touchPos))))
                { 
                    
                    case var value when value == GridBuilding.tileBases[TileType.Fire]: //Case Feu
                        TooltipCases.ShowToolTip_Static("<b>Tile Feu</b> \n Augmente les dégâts des tours adjacentes.");
                        break;
                    
                    case var value when value == GridBuilding.tileBases[TileType.Water]: //Case Eau
                        TooltipCases.ShowToolTip_Static("<b>Tile Eau</b> \n Augmente la cadence des tours adjacentes.");
                        break;
                    
                    case var value when value == GridBuilding.tileBases[TileType.Earth]: //Case Earth
                        TooltipCases.ShowToolTip_Static("<b>Tile Terre</b> \n Augmente la portée des tours adjacentes.");
                        break;
                    
                    case var value when value == GridBuilding.tileBases[TileType.Grey]: //Case Batiment
                        
                        GridBuilding.current.listeTowerCo.TryGetValue(Vector3Int.FloorToInt(GridBuilding.current.gridLayout.LocalToCell(touchPos)), out var tower);
                        TowerStats towerStats = tower?.GetComponentInChildren<TowerStats>();
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            if (towerStats != null)
                            {
                                TooltipComplet.HideTooltip_Static();
                                TooltipExtented.ShowToolTip_Static(
                                    towerStats.ameliorations.Count,
                                    towerStats.ameliorations,
                                    Mathf.RoundToInt(towerStats.health) + "/" + Mathf.RoundToInt(towerStats.maxHealth),
                                    towerStats.damages,
                                    towerStats.cadence,
                                    towerStats.radius,
                                    towerStats.fireSurrounding,
                                    towerStats.waterSurrouding,
                                    towerStats.earthSurrounding
                                );
                            }
                        }
                        else
                        {
                            if (towerStats != null)
                            {
                                TooltipExtented.HideTooltip_Static();
                                TooltipComplet.ShowToolTip_Static(
                                towerStats.ameliorations.Count,
                                towerStats.ameliorations,
                                Mathf.RoundToInt(towerStats.health) + "/" + Mathf.RoundToInt(towerStats.maxHealth),
                                towerStats.damages,
                                towerStats.cadence,
                                towerStats.radius
                            );
                            }
                            
                        }
                        break;
                    
                    default :  
                        Debug.Log("TileAutre");
                        break;
                }
                
            }
        }
        else
        {
            TooltipComplet.HideTooltip_Static();
            TooltipExtented.HideTooltip_Static();
            TooltipCases.HideTooltip_Static();
        }
        
    }
}
