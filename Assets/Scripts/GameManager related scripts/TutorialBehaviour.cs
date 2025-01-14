using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{

    [Header("GD ne pas toucher")]
    public RessourcesManager RM;
    public Spawn spawn;
    public GameObject launchWaveButton;
    public GameObject ingamePanel;
    public GameObject alchemyPanel;

    private bool AlchemyButtonCkicked = false;
    private bool alreadyOneSpellCoocked = false;
    private bool waveOnebeginned = false, waveTwobeginned = false;
    private bool waveOneFinished = false, waveTwoFinished = false;
    private bool playerPlacedATower = false;
    private bool playerFinishedPlacingTower = false;
    private bool playerClickedOnAlchemyButton = false;
    private bool playerCoockedASpell = false;
    private bool playerPlacedASpell = false;

    public int tutorialStep = 0;

    public static bool isInTutorial;
    public static bool cameraLocked = false;
    public static float tutorialTimeScale = 1f;

    public GameObject textBox;
    public GameObject highLighter;
    public Sprite[] tomSprites;
    public Sprite[] arthurSprites;

    [Header("espace GD")]
    public GameObject bottomPosition;
    [TextArea(4, 4)] public string[] tutorialText;
    public GameObject[] objectsToHideAndShow;


    private void Awake()
    {
        RM = FindObjectOfType<RessourcesManager>();
        spawn = FindObjectOfType<Spawn>();
    }


    void Start() //étape 0 du tutoriel
    {
        tutorialStep = 0;
        ResumeTutorial();
        MoveTextBox(bottomPosition.transform.position);
        ModifyTextBox(tutorialText[tutorialStep]);
        ModifySpeakingCharacter(1);
        HideCharacter(2);
        StopHighlighting();
        ShowTextBox();
        ChangeExpression(1, 23);
        DesactivateAllGameObjects();
        LockCamera();
        ModifyTextBoxScale(650f, 85f);
        DesactivateGameObject(6);
    }

    private bool isCoroutineRunning = false;

    private void Update()
    {
        if (RM.spellSlotOne.Count != 0) alreadyOneSpellCoocked = true;

        if (RM.wave == 1 && launchWaveButton.activeSelf && !isCoroutineRunning)
        {
            isCoroutineRunning = true;
            StartCoroutine(WaitForMonsterToSpawn(() => waveOnebeginned = true));
        }

        if (RM.wave == 2 && launchWaveButton.activeSelf && !isCoroutineRunning)
        {
            isCoroutineRunning = true;
            StartCoroutine(WaitForMonsterToSpawn(() => waveTwobeginned = true));
        }

        if (tutorialStep == 16 && waveOnebeginned && launchWaveButton.activeSelf)
        {
            tutorialStep++;
            NextStep();
            waveOneFinished = true;
        }

        if (tutorialStep == 29 && waveTwobeginned && launchWaveButton.activeSelf)
        {
            tutorialStep++;
            NextStep();
            waveTwoFinished = true;
        }

        if (GridBuilding.current.listeTowerCo.Count != 0)
        {
            playerPlacedATower = true;
        }

        if (tutorialStep == 10 && playerPlacedATower)
        {
            ShowNextButton();
        }

        if (RM.fireSoul == 90 && RM.waterSoul == 90 && RM.plantSoul == 90)
        {
            playerFinishedPlacingTower = true;
        }

        if (tutorialStep == 13 && playerFinishedPlacingTower)
        {
            ShowNextButton();
        }

        if (RM.spellSlotOne.Count > 0)
        {
            playerCoockedASpell = true;
        }

        if (tutorialStep == 23 && playerCoockedASpell)
        {
            ShowNextButton();
        }

        if (EndGameStats.EGS.nombreDeSortsPlaces > 0)
        {
            playerPlacedASpell = true;
        }

        if (tutorialStep == 24 && playerPlacedASpell)
        {
            ShowNextButton();
            StopHighlighting();
        }
    }

    private IEnumerator WaitForMonsterToSpawn(Action onWaveBeginned)
    {
        yield return new WaitForSeconds(2f);
        onWaveBeginned();
        isCoroutineRunning = false;
    }

    public void waveButtonCalled()
    {
        if (tutorialStep == 15 || tutorialStep == 28)
        {
            tutorialStep++;
            NextStep();
        }
    }



    public void AlchemyButtonCalled()
    {
        AlchemyButtonCkicked = true;
        if (tutorialStep == 20)
        {
            tutorialStep++;
            NextStep();
            playerClickedOnAlchemyButton = true;
        }
    }



    public void ShowTextBox() //affiche toute la textboxe , personnages compris
    {
        textBox.SetActive(true);
    }

    public void HideTextBox() //cache toute la textboxe , personnages compris
    {
        textBox.SetActive(false);
    }

    public void HideNextButton()
    {
        textBox.transform.GetChild(4).gameObject.SetActive(false);
    }
    public void ShowNextButton()
    {
        textBox.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void ModifyToCurentText() //change le text de la text boxe pour celui contenu dans le tutorialtext de l'etape actuel
    {
        if (tutorialStep < tutorialText.Length) ModifyTextBox(tutorialText[tutorialStep]);
    }

    public void ModifyTextBox(string text = "") //moifie le text de la textboxe pour nimporte quel string (ne sert a rien en théorie a part pour bypass la fonction ci dessus. par défaut renvoie un texte vide
    {
        textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    public void MoveTextBox(Vector3 newPosition) //déplace la textboxe en entier (personnages compris) au coordonnées qu'on lui donne (pour utiliser avec un gameObject comme repere, utiliser comme ceci : MoveTextBox(GameObject.transform.position));
    {
        textBox.transform.position = newPosition;
    }

    public void ModifyTextBoxScale(float newWidth = 650f, float newHeight = 155f)// Modifie la taille de la zone de texte et déplace les deux personnages
    {
        textBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, newHeight);
        textBox.transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth - 150, newHeight - 150);

        Transform childTransform2 = textBox.transform.GetChild(2);
        Vector3 currentPosition2 = childTransform2.position;
        currentPosition2.y = textBox.transform.position.y + textBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 + 98;
        childTransform2.position = currentPosition2;

        Transform childTransform3 = textBox.transform.GetChild(3);
        Vector3 currentPosition3 = childTransform3.position;
        currentPosition3.y = textBox.transform.position.y + textBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 + 105;
        childTransform3.position = currentPosition3;

        Transform childTransform4 = textBox.transform.GetChild(4);
        Vector3 currentPosition4 = childTransform4.position;
        currentPosition4.y = textBox.transform.position.y - (textBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y / 2) - 2;
        childTransform4.position = currentPosition4;
    }


    public void DesactivateGameObject(int gameObjectToDesactivate) //désactive n'importe quel gameobject renseigné dans le tableau "objectsToHideAndShow" dans l'inpecteur. prend pour parametre le numero de l'element en question dans l'inpecteur a partir de 0
    {
        objectsToHideAndShow[gameObjectToDesactivate].SetActive(false);
    }

    public void ActivateGameobject(int gameObjectToActivate)// active n'importe quel gameobject renseigné dans le tableau "objectsToHideAndShow" dans l'inpecteur. prend pour parametre le numero de l'element en question dans l'inpecteur a partir de 0
    {
        objectsToHideAndShow[gameObjectToActivate].SetActive(true);
    }

    public void DesactivateAllGameObjects() // désactive tout les gameObjects renseignés dans le tableau "objectsToHideAndShow"
    {
        foreach (GameObject objectToDesactivate in objectsToHideAndShow)
        {
            objectToDesactivate.SetActive(false);
        }
    }

    public void ActivateAllGameObjects() // active tout les gameObjects renseignés dans le tableau "objectsToHideAndShow"
    {
        foreach (GameObject objectToActivate in objectsToHideAndShow)
        {
            objectToActivate.SetActive(true);
        }
    }

    public void ModifySpeakingCharacter(int newSpeakingCharacter) //change l'intensité du personnage selectionné pour 1 et met en semi-transparent le deuxieme. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (newSpeakingCharacter == 1)
        {
            var image2 = textBox.transform.GetChild(2).GetComponent<Image>();
            var color2 = image2.color;
            color2.a = 1f;
            image2.color = color2;

            var image3 = textBox.transform.GetChild(3).GetComponent<Image>();
            var color3 = image3.color;
            color3.a = 0.5f;
            image3.color = color3;
        }
        else
        {
            var image3 = textBox.transform.GetChild(3).GetComponent<Image>();
            var color3 = image3.color;
            color3.a = 1f;
            image3.color = color3;

            var image2 = textBox.transform.GetChild(2).GetComponent<Image>();
            var color2 = image2.color;
            color2.a = 0.5f;
            image2.color = color2;
        }
    }

    public void HideCharacter(int characterToHide) // cache le personnage sélectionné. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToHide == 1) textBox.transform.GetChild(2).gameObject.SetActive(false);
        else if (characterToHide == 2) textBox.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void ShowCharacter(int characterToShow) // affiche le personnage sélectionné. le personnage de gauche est désigné par 1 et celui de droite par 2 (ne concerne pas l'intensité de celui ci)
    {
        if (characterToShow == 1) textBox.transform.GetChild(2).gameObject.SetActive(true);
        else if (characterToShow == 2) textBox.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void PlaceCharacterInCenter(int characterToPlace) // place le personnage désigné seul au centre de la textboxe. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToPlace == 1)
        {
            HideCharacter(2);
            textBox.transform.GetChild(2).position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 180, textBox.transform.position.z);
        }
        else
        {
            HideCharacter(1);
            textBox.transform.GetChild(3).position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 180, textBox.transform.position.z);
        }
    }

    public void BackToDialogue() //utile uniquement si on a emplyé la fonction "PlaceCharacterInCenter(characterToPlace)". replace les deux personnages a leurs positions d'origine
    {
        if (textBox.transform.GetChild(2).gameObject.activeSelf)
        {
            ShowCharacter(2);
            textBox.transform.GetChild(2).position = new Vector3(textBox.transform.position.x - 255, textBox.transform.position.y + 130, textBox.transform.position.z);
        }

        if (textBox.transform.GetChild(3).gameObject.activeSelf)
        {
            ShowCharacter(1);
            textBox.transform.GetChild(3).position = new Vector3(textBox.transform.position.x + 255, textBox.transform.position.y + 130, textBox.transform.position.z);
        }
    }

    public void ChangeExpression(int characterToChange, int newExpression) // change l'expression de visage du personnage sélectionné selon un indice. les expressions sont dans l'ordre dans les dossiers dans lesquelles elles sont référencées. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToChange == 1 && newExpression <= tomSprites.Length) textBox.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = tomSprites[newExpression];
        if (characterToChange == 2 && newExpression <= arthurSprites.Length) textBox.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = arthurSprites[newExpression];
    }

    public void Highlight(Vector2 highLightPosition, Vector2 highLightScale) // affiche un cadre roue clignotant la la position indiquée en parametre 1 (coordonées x/y) et de la taille indiquée en parametre 2 (largeur/hauteur)
    {
        highLighter.SetActive(true);
        RectTransform rectTransform = highLighter.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Debug.Log("Highlight");
            rectTransform.anchoredPosition = highLightPosition;
            rectTransform.localScale = highLightScale;
        }
    }


    public void StopHighlighting() // retire le cadre clignotant de l'écran.
    {
        highLighter.SetActive(false);
    }

    public void ModifyTimeScale(float newTimeScale = 1f) // change la vitesse de l'écoulement du temps (bypass l'option de fast mode du gameManager). par défaut met la vitesse de jeu en normale
    {
        tutorialTimeScale = newTimeScale;
        Time.timeScale = newTimeScale;
    }

    public void LockCamera()
    {
        cameraLocked = true;
    }

    public void UnlockCamera()
    {
        cameraLocked = false;
    }

    public void StopTutorial() // arrete le tutoriel, retire la texteboxe, retire le carde clignotant, permet au joueur d'utiliser les raccourcis et le fast mode. réactive tout les gameObject du tableau "objectsToHideAndShow"
    {
        isInTutorial = false;
        HideTextBox();
        StopHighlighting();
        ActivateAllGameObjects();
        tutorialTimeScale = 1f;
    }

    public void ResumeTutorial()
    {
        isInTutorial = true;
        ShowTextBox();
    }

    public void MakeUninteractable(List<int> gameObjectToUninteractable) 
    {
        foreach (var objectToUninteractable in gameObjectToUninteractable)
        {
            objectsToHideAndShow[objectToUninteractable].GetComponent<Button>().enabled = false;
        }
    }
    
    public void MakeInteractable(List<int> gameObjectToInteractable) 
    {
        foreach (var objectToInteractable in gameObjectToInteractable)
        {
            objectsToHideAndShow[objectToInteractable].GetComponent<Button>().enabled = true;
        }
        
    }
    public void NextStep()
    {

        if (tutorialStep < 10) tutorialStep++;
        if (tutorialStep == 10 && GridBuilding.current.listeTowerCo.Count > 0) tutorialStep++;
        if (tutorialStep > 10 && tutorialStep < 13) tutorialStep++;
        if (tutorialStep == 13 && RM.fireSoul == 90 && RM.waterSoul == 90 && RM.plantSoul == 90) tutorialStep++;
        if (tutorialStep > 13 && tutorialStep < 15) tutorialStep++;
        if (tutorialStep == 17 && waveOneFinished) tutorialStep++;
        if (tutorialStep > 17 && tutorialStep < 20) tutorialStep++;
        if (tutorialStep == 21 && playerClickedOnAlchemyButton) tutorialStep++;
        if (tutorialStep > 21 && tutorialStep < 23) tutorialStep++;
        if (tutorialStep == 23 && alreadyOneSpellCoocked) tutorialStep++;
        if (tutorialStep == 24 && EndGameStats.EGS.nombreDeSortsPlaces > 0) tutorialStep++;
        if (tutorialStep > 24 && tutorialStep < 28) tutorialStep++;
        if (tutorialStep == 30 && waveTwoFinished) tutorialStep++;
        if (tutorialStep > 30) tutorialStep++;

        



        switch (tutorialStep)
        {
            case 0:
                break;

            case 1:
                ChangeExpression(2, 2);
                ShowCharacter(2);
                LockCamera();
                ModifySpeakingCharacter(2); // Premier dialogue ou Arthur parle
                ModifyToCurentText();
                ModifyTextBoxScale(650f, 135f);
                Debug.Log("Arthur parle");
                break;

            case 2:
                ChangeExpression(2, 19);
                ChangeExpression(1, 18);
                ModifyToCurentText();
                ModifyTextBoxScale();
                break;
            case 3:
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 3);
                ModifyToCurentText();
                ModifyTextBoxScale(650f, 75f);
                MoveTextBox(new Vector3(bottomPosition.transform.position.x, bottomPosition.transform.position.y + 20, 0));
                break;
            case 4:
                ChangeExpression(1, 17);
                ModifyToCurentText();
                ActivateGameobject(10); // Affiche le SoulsDisplay pour montrer les ressources
                Highlight(new Vector2(-831, -380), new Vector2(3f, 3.8f)); // Highlight des Âmes
                ModifyTextBoxScale(650f, 165f);
                break;
            case 5:
                ModifySpeakingCharacter(1);
                ModifyToCurentText();
                StopHighlighting();
                ModifyTextBoxScale(650f, 130f);
                ChangeExpression(2, 9);
                break;
            case 6: // Les Terrains
                ModifyToCurentText();
                Highlight(new Vector2(-105, -260), new Vector2(2.5f, 1.25f));
                ModifyTextBoxScale(650f, 55f);
                break; //
            case 7:
                ModifyToCurentText();
                Highlight(new Vector2(490, -55), new Vector2(1.25f, 1.25f));
                break;
            case 8:
                ModifyToCurentText();   
                Highlight(new Vector2(-272, 380), new Vector2(1.25f, 1.25f));
                ModifyTextBoxScale(650f, 105f);
                break;

            case 9:
                ShowTextBox();
                ModifyToCurentText();
                ChangeExpression(2, 2);
                ModifyTextBoxScale(650f, 135f);
                StopHighlighting();
                ModifySpeakingCharacter(2);
                ModifyTextBoxScale(650f, 135f);
                break;
            // Le joueur doit placer une tour avant de pouvoir poursuivre.
            case 10:
                ModifyToCurentText();
                ActivateGameobject(0); // Affiche les différentes tours et leurs prix
                ActivateGameobject(1);
                ActivateGameobject(2);
                ActivateGameobject(3);
                ActivateGameobject(4);
                ActivateGameobject(5);
                ModifySpeakingCharacter(2);
                ChangeExpression(1, 3);
                Highlight(new Vector2(-120, -425), new Vector2(5, 2.7f)); // Highlight des différentes tours
                MoveTextBox(new Vector3(1580, 150, 0));
                ModifyTextBoxScale(650f, 205f);
                HideNextButton();
                break;
            case 11:
                ModifyToCurentText(); // Amélioration de tour, qu'il n'est pas obligé de faire.
                ModifyTextBoxScale(650f, 165f);
                StopHighlighting();
                break;
            case 12:
                MakeUninteractable(new List<int> { 1,3,5});
                ModifyToCurentText(); // Suppression de tour
                ChangeExpression(2, 8);
                ModifySpeakingCharacter(2);
                MoveTextBox(new Vector3(1605, 150, 0));
                Highlight(new Vector2(161, -481), new Vector2(0.9f, 0.9f));
                ActivateGameobject(9); // Affiche le bouton de Suppression
                ModifyTextBoxScale(650f, 250f);
                break;
            case 13:
                UnlockCamera();
                MakeInteractable(new List<int> { 1,3,5});
                StopHighlighting();
                ModifyToCurentText();
                ModifySpeakingCharacter(2);
                ModifyTextBoxScale(675f, 260f);
                ChangeExpression(1, 23);
                HideNextButton();
                break;
            // Le joueur doit finir de placer ces défenses (Jusqu'à qu'il n'ait plus de ressources pour placer ses ressources dans les 3 éléments=
            case 14:
                
                ModifyToCurentText();
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 3);
                ModifySpeakingCharacter(1);
                ModifyTextBoxScale(650f, 55f);
                break;
            case 15:
                MakeUninteractable(new List<int> { 1,3,5});
                ActivateGameobject(17);
                ModifyToCurentText();
                MoveTextBox(new Vector3(1605,718, 0));
                ChangeExpression(1, 18);
                ModifySpeakingCharacter(1);
                ChangeExpression(2,21);
                ActivateGameobject(6);
                ActivateGameobject(10);
                ModifyTextBoxScale(650f, 135f);
                Highlight(new Vector2(805, 452.5f), new Vector2(3.2f, 0.35f));
                HideNextButton();
                ActivateGameobject(13); // A désactiver une fois fix
                break; // La première vague
            case 16:
                StopHighlighting();
                UnlockCamera();
                HideTextBox();
                break;
            case 17:
                DesactivateGameObject(17);
                ShowNextButton();
                ShowTextBox(); // Post Vague 1
                ModifySpeakingCharacter(2);
                MoveTextBox(new Vector3(950, 500, 0));
                StopHighlighting();
                ChangeExpression(1, 23);
                ChangeExpression(2, 2);
                ModifyToCurentText();
                LockCamera();
                ModifyTextBoxScale(675f, 135f);
                break;
            case 18:
                ChangeExpression(1, 8);
                ModifyToCurentText();
                ModifyTextBoxScale(650f, 85f);
                break;
            case 19:
                ChangeExpression(2, 2);
                ModifyToCurentText();
                break;
            case 20:
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 17);
                ModifyToCurentText();
                ModifyTextBoxScale();
                ActivateGameobject(7); // Affiche le bouton d'Alchemy
                Highlight(new Vector2(-860, 319), new Vector2(1f, 0.9f));
                // Le joueur doit cliquer sur le bouton d'Alchemy pour continuer
                HideNextButton();
                break;
            case 21:
                ShowTextBox();
                ModifyToCurentText();
                Highlight(new Vector2(-518, -38), new Vector2(3.86f, 1.7f)); // Forme du Sort
                MoveTextBox(new Vector3(1400, 320));
                ModifyTextBoxScale(650f, 200f);
                ShowNextButton();
                break;
            case 22:
                ChangeExpression(1, 8);
                ModifyToCurentText();
                MoveTextBox(new Vector3(1550, 450));
                ModifyTextBoxScale(650f, 175f);
                Highlight(new Vector2(-430, -37), new Vector2(1.5f, 1.5f)); // Terrain lié à la forme
                ShowNextButton();
                break;
            case 23:
                ModifyToCurentText();
                UnlockCamera(); // Unlock de caméra pour qu'il puisse placer le sort ou qu'il le souhaite.
                ModifySpeakingCharacter(2);
                ChangeExpression(1, 17);
                Highlight(new Vector2(-170, -90), new Vector2(1.8f, 0.3f));
                HideNextButton();
                UnlockCamera();
                // Le joueur doit cook un sort pour continuer.
                break;
            case 24:
                MenuManager.activePanel = ingamePanel;
                alchemyPanel.SetActive(false);
                ChangeExpression(1, 20);
                ModifySpeakingCharacter(1);
                ModifyToCurentText();
                Highlight(new Vector2(906, -187), new Vector2(1.26f, 1.7f));
                ModifyTextBoxScale(650f, 170f);
                HideNextButton();
                UnlockCamera();
                // Le joueur place ensuite son sort nouvellement créer qu'il doit placer pour pouvoir continuer
                break;
            case 25:
                ShowTextBox();
                UnlockCamera();
                StopHighlighting();
                ModifyToCurentText();
                ModifyTextBoxScale(650f, 85f);
                break;
            case 26:
                ChangeExpression(1, 3); // Infos tooltips;
                ChangeExpression(2, 23);
                ModifyToCurentText();
                LockCamera();
                ModifyTextBoxScale(650f, 175f);
                ActivateGameobject(16);
                break;
            case 27:
                ChangeExpression(1, 18);
                ChangeExpression(2, 24);
                ModifyToCurentText();
                StopHighlighting();
                ModifySpeakingCharacter(2);
                ActivateGameobject(12); // Activation Triangle Barry
                Highlight(new Vector2(545, 470), new Vector2(1.75f, 1.4f));
                ModifyTextBoxScale(685f, 250f);
                break;
            case 28:
                MakeInteractable(new List<int> { 1,3,5});
                ActivateGameobject(17);
                Highlight(new Vector2(805, 452.5f), new Vector2(3.2f, 0.35f));
                ModifyToCurentText();
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 14);
                ChangeExpression(2, 18);
                HideNextButton();
                ModifyTextBoxScale(650f, 85f);
                // Le joueur doit lancer la prochaine vague pour continuer.
                break;
            case 29:
                DesactivateGameObject(17);  
                StopHighlighting();
                UnlockCamera();
                HideTextBox();
                ModifyTextBoxScale(685f, 250f);
                break;
            case 30:
                // SoulConverter display
                
                ShowTextBox();
                LockCamera();
                ModifyToCurentText();
                ModifySpeakingCharacter(2);
                ChangeExpression(2, 8);
                ChangeExpression(1, 23);
                ActivateGameobject(8);
                ModifyTextBoxScale(680f, 265f);
                Highlight(new Vector2(-864, 228), new Vector2(2.2f, 1.3f));
                ShowNextButton();
                break;
            case 31: // In SoulConverter Menu
                ModifyToCurentText();
                // Highlight(new Vector2(-765, 50), new Vector2(3.25f, 1.4f));
                break;
            case 32:
                // Bouquin
                Highlight(new Vector2(-861, 117), new Vector2(2.1f, 1.27f));
                ModifyToCurentText();
                ModifySpeakingCharacter(1);
                ChangeExpression(2,2);
                ActivateGameobject(13);
                ActivateGameobject(14);
                ModifyTextBoxScale(680f, 175f);
                break;
            case 33:
                ModifySpeakingCharacter(2);
                ChangeExpression(2, 9);
                ChangeExpression(1, 8);
                PlaceCharacterInCenter(2);
                StopHighlighting();
                ModifyToCurentText();   
                ActivateAllGameObjects();
                UnlockCamera();
                ModifyTextBoxScale();
                ModifyTextBoxScale(650f, 180f);
                break;
            case 34:
                ActivateGameobject(17);
                StopTutorial();
                break;
        }
    }

}
