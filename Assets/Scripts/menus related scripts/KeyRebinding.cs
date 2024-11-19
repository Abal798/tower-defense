using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyRebinding : MonoBehaviour
{
    [System.Serializable]
    public class KeyBinding
    {
        public string actionName; // Nom de l'action (ex: "Jump")
        public KeyCode key;       // Touche actuelle assignée
        public Button rebindButton; // Bouton de réassignation dans l'UI
        public TextMeshProUGUI displayText;  // Texte affichant la touche actuelle
    }

    public List<KeyBinding> keyBindings = new List<KeyBinding>();
    private KeyBinding currentBinding; // Binding en cours de réassignation

    private void Awake()
    {
        LoadBindings();
    }

    void Start()
    {
        

        // Initialiser l'interface avec les touches actuelles
        foreach (var binding in keyBindings)
        {
            // Assurez-vous que chaque bouton est correctement configuré
            binding.displayText.text = binding.key.ToString();
            binding.rebindButton.onClick.RemoveAllListeners();
            binding.rebindButton.onClick.AddListener(() => StartRebind(binding));
        }
    }

    void Update()
    {
        // Si une réassignation est en cours, capter la touche pressée
        if (currentBinding != null)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    // Assigner la nouvelle touche
                    currentBinding.key = key;
                    currentBinding.displayText.text = key.ToString();

                    // Fin de la réassignation
                    currentBinding = null;
                    break;
                }
            }
        }
    }

    void StartRebind(KeyBinding binding)
    {
        // Indique que ce binding est en cours de modification
        currentBinding = binding;
        binding.displayText.text = "Press a Key...";
    }

    public KeyCode GetKeyForAction(string actionName)
    {
        // Retourne la touche assignée pour une action donnée
        var binding = keyBindings.Find(b => b.actionName == actionName);
        return binding != null ? binding.key : KeyCode.None;
    }

    void LoadBindings()
    {
        foreach (var binding in keyBindings)
        {
            string savedKey = PlayerPrefs.GetString(binding.actionName, binding.key.ToString());
            if (System.Enum.TryParse(savedKey, out KeyCode key))
            {
                binding.key = key;
                binding.displayText.text = key.ToString();
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveBindings();
    }

    void SaveBindings()
    {
        foreach (var binding in keyBindings)
        {
            PlayerPrefs.SetString(binding.actionName, binding.key.ToString());
        }
        PlayerPrefs.Save();
    }
}
