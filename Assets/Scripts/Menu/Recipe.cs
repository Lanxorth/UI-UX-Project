using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Book/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeTitle;
    public string[] ingredients;
    [TextArea(3, 10)]
    public string description;
}
