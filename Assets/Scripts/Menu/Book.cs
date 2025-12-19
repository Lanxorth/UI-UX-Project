using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("UI Pages")]
    public TMP_Text leftTitle;
    public TMP_Text leftIngredients;
    public TMP_Text leftDescription;

    public TMP_Text rightTitle;
    public TMP_Text rightIngredients;
    public TMP_Text rightDescription;

    [Header("Page Buttons")]
    public Button leftPageButton;
    public Button rightPageButton;

    [Header("Navigation Buttons")]
    public Button nextButton;
    public Button prevButton;
    public Button addButton; // bouton général pour ajouter une recette

    [Header("View Popup")]
    public CanvasGroup viewPopup;
    public TMP_Text popupTitle;
    public TMP_Text popupIngredients;
    public TMP_Text popupDescription;
    public Button closeViewButton;

    [Header("Add Popup")]
    public CanvasGroup addPopup;
    public TMP_InputField inputTitle;
    public TMP_InputField inputDescription;
    public TMP_Dropdown ingredientDropdown1;
    public TMP_Dropdown ingredientDropdown2;
    public TMP_Dropdown ingredientDropdown3;
    public TMP_Dropdown ingredientDropdown4;
    public Button saveButton;
    public Button cancelButton;

    [Header("Recipes")]
    public List<Recipe> recipes = new List<Recipe>();

    private int pageIndex = 0;

    [HideInInspector] public bool isPopupOpen = false;

    void Start()
    {
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        addButton.onClick.AddListener(OpenAddPopup);

        closeViewButton.onClick.AddListener(CloseViewPopup);
        cancelButton.onClick.AddListener(CloseAddPopup);
        saveButton.onClick.AddListener(SaveNewRecipe);

        // Désactive les popups au départ
        if (addPopup != null) addPopup.gameObject.SetActive(false);
        if (viewPopup != null) viewPopup.gameObject.SetActive(false);

        UpdatePages();
    }

    #region Pages
    void UpdatePages()
    {
        // Page gauche
        Recipe leftRecipe = pageIndex < recipes.Count ? recipes[pageIndex] : null;
        DisplayRecipeOnPage(leftRecipe, leftTitle, leftIngredients, leftDescription);
        SetupPageButton(leftPageButton, leftRecipe);

        // Page droite
        Recipe rightRecipe = pageIndex + 1 < recipes.Count ? recipes[pageIndex + 1] : null;
        DisplayRecipeOnPage(rightRecipe, rightTitle, rightIngredients, rightDescription);
        SetupPageButton(rightPageButton, rightRecipe);

        // Mise à jour boutons de navigation
        prevButton.interactable = pageIndex > 0;
        nextButton.interactable = pageIndex + 2 < recipes.Count;
    }

    void DisplayRecipeOnPage(Recipe r, TMP_Text title, TMP_Text ing, TMP_Text desc)
    {
        if (title == null || ing == null || desc == null) return;

        if (r == null)
        {
            ClearPage(title, ing, desc);
            return;
        }

        title.text = r.recipeTitle ?? "";
        ing.text = r.ingredients != null ? string.Join("\n", r.ingredients) : "";
        desc.text = r.description ?? "";
    }

    void ClearPage(TMP_Text title, TMP_Text ing, TMP_Text desc)
    {
        if (title != null) title.text = "[Add Recipe]";
        if (ing != null) ing.text = "";
        if (desc != null) desc.text = "";
    }

    void SetupPageButton(Button button, Recipe r)
    {
        if (button == null) return;

        button.onClick.RemoveAllListeners();

        if (r == null)
        {
            button.onClick.AddListener(OpenAddPopup);
        }
        else
        {
            button.onClick.AddListener(() => ShowViewPopup(r));
        }
    }

    void NextPage()
    {
        pageIndex += 2;
        if (pageIndex >= recipes.Count)
            pageIndex = Mathf.Max(0, recipes.Count - 2);

        UpdatePages();
    }

    void PrevPage()
    {
        pageIndex -= 2;
        if (pageIndex < 0) pageIndex = 0;

        UpdatePages();
    }
    #endregion

    #region View Popup
    void ShowViewPopup(Recipe r)
    {
        if (r == null || viewPopup == null) return;

        viewPopup.gameObject.SetActive(true); // Activer le GameObject

        popupTitle.text = r.recipeTitle;
        popupIngredients.text = r.ingredients != null ? string.Join("\n", r.ingredients) : "";
        popupDescription.text = r.description;

        viewPopup.alpha = 1;
        viewPopup.interactable = true;
        viewPopup.blocksRaycasts = true;

        isPopupOpen = true;
    }

    void CloseViewPopup()
    {
        if (viewPopup == null) return;

        viewPopup.alpha = 0;
        viewPopup.interactable = false;
        viewPopup.blocksRaycasts = false;

        viewPopup.gameObject.SetActive(false); // Désactiver le GameObject

        isPopupOpen = false;
    }
    #endregion

    #region Add Popup
    void OpenAddPopup()
    {
        if (addPopup == null) return;

        addPopup.gameObject.SetActive(true); // Activer le GameObject

        inputTitle.text = "";
        inputDescription.text = "";

        if (ingredientDropdown1 != null) ingredientDropdown1.value = 0;
        if (ingredientDropdown2 != null) ingredientDropdown2.value = 0;
        if (ingredientDropdown3 != null) ingredientDropdown3.value = 0;
        if (ingredientDropdown4 != null) ingredientDropdown4.value = 0;

        addPopup.alpha = 1;
        addPopup.interactable = true;
        addPopup.blocksRaycasts = true;

        isPopupOpen = true;
    }

    void CloseAddPopup()
    {
        if (addPopup == null) return;

        addPopup.alpha = 0;
        addPopup.interactable = false;
        addPopup.blocksRaycasts = false;

        addPopup.gameObject.SetActive(false); // Désactiver le GameObject

        isPopupOpen = false;
    }

    void SaveNewRecipe()
    {
        if (addPopup == null) return;

        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
        newRecipe.recipeTitle = inputTitle.text;
        newRecipe.description = inputDescription.text;

        List<string> selectedIngredients = new List<string>();
        if (ingredientDropdown1 != null) selectedIngredients.Add(ingredientDropdown1.options[ingredientDropdown1.value].text);
        if (ingredientDropdown2 != null) selectedIngredients.Add(ingredientDropdown2.options[ingredientDropdown2.value].text);
        if (ingredientDropdown3 != null) selectedIngredients.Add(ingredientDropdown3.options[ingredientDropdown3.value].text);
        if (ingredientDropdown4 != null) selectedIngredients.Add(ingredientDropdown4.options[ingredientDropdown4.value].text);

        newRecipe.ingredients = selectedIngredients.ToArray();

        recipes.Add(newRecipe);
        UpdatePages();
        CloseAddPopup();
    }
    #endregion
}
