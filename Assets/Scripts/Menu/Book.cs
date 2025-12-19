using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("UI")]
    public Canvas worldCanvas;
    public TMP_Text leftPageTitle;
    public TMP_Text leftPageIngredients;
    public TMP_Text leftPageDescription;

    public TMP_Text rightPageTitle;
    public TMP_Text rightPageIngredients;
    public TMP_Text rightPageDescription;

    public Button nextPageButton;
    public Button prevPageButton;

    [Header("Modals")]
    public CanvasGroup viewRecipePopup;  // pour lire la recette
    public TMP_Text popupTitle;
    public TMP_Text popupIngredients;
    public TMP_Text popupDescription;
    public Button closePopupButton;

    public CanvasGroup addRecipePopup;   // pour ajouter une recette
    public TMP_InputField inputTitle;
    public TMP_InputField inputDescription;
    public Dropdown inputIngredients;
    public Button saveRecipeButton;
    public Button cancelAddRecipeButton;

    [Header("Recipes")]
    public List<Recipe> recipes = new List<Recipe>();

    private int currentPageIndex = 0; // page gauche = index, page droite = index+1

    void Start()
    {
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PrevPage);

        closePopupButton.onClick.AddListener(CloseViewPopup);
        cancelAddRecipeButton.onClick.AddListener(CloseAddPopup);
        saveRecipeButton.onClick.AddListener(SaveNewRecipe);

        UpdatePages();
    }

    #region Pagination
    void UpdatePages()
    {
        // gauche
        if (currentPageIndex < recipes.Count)
        {
            leftPageTitle.text = recipes[currentPageIndex].recipeTitle;
            leftPageIngredients.text = string.Join("\n", recipes[currentPageIndex].ingredients);
            leftPageDescription.text = recipes[currentPageIndex].description;
        }
        else
        {
            leftPageTitle.text = "";
            leftPageIngredients.text = "";
            leftPageDescription.text = "";
        }

        // droite
        if (currentPageIndex + 1 < recipes.Count)
        {
            rightPageTitle.text = recipes[currentPageIndex + 1].recipeTitle;
            rightPageIngredients.text = string.Join("\n", recipes[currentPageIndex + 1].ingredients);
            rightPageDescription.text = recipes[currentPageIndex + 1].description;
        }
        else
        {
            rightPageTitle.text = "";
            rightPageIngredients.text = "";
            rightPageDescription.text = "";
        }
    }

    void NextPage()
    {
        if (currentPageIndex + 2 < recipes.Count)
        {
            currentPageIndex += 2;
            UpdatePages();
        }
    }

    void PrevPage()
    {
        if (currentPageIndex - 2 >= 0)
        {
            currentPageIndex -= 2;
            UpdatePages();
        }
    }
    #endregion

    #region View Popup
    public void ShowRecipePopup(Recipe recipe)
    {
        if (recipe == null) return;

        popupTitle.text = recipe.recipeTitle;
        popupIngredients.text = string.Join("\n", recipe.ingredients);
        popupDescription.text = recipe.description;

        viewRecipePopup.alpha = 1;
        viewRecipePopup.interactable = true;
        viewRecipePopup.blocksRaycasts = true;
    }

    void CloseViewPopup()
    {
        viewRecipePopup.alpha = 0;
        viewRecipePopup.interactable = false;
        viewRecipePopup.blocksRaycasts = false;
    }
    #endregion

    #region Add Popup
    public void OpenAddPopup()
    {
        inputTitle.text = "";
        inputDescription.text = "";
        inputIngredients.value = 0;

        addRecipePopup.alpha = 1;
        addRecipePopup.interactable = true;
        addRecipePopup.blocksRaycasts = true;
    }

    void CloseAddPopup()
    {
        addRecipePopup.alpha = 0;
        addRecipePopup.interactable = false;
        addRecipePopup.blocksRaycasts = false;
    }

    void SaveNewRecipe()
    {
        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
        newRecipe.recipeTitle = inputTitle.text;
        newRecipe.description = inputDescription.text;
        newRecipe.ingredients = new string[] { inputIngredients.options[inputIngredients.value].text };

        recipes.Add(newRecipe);
        UpdatePages();
        CloseAddPopup();
    }
    #endregion
}
