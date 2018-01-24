using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private bool matchFound = false;

	void Awake() {
		render = GetComponent<SpriteRenderer>();
    }

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
		SFXManager.instance.PlaySFX(Clip.Select);
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}
    private void OnMouseDown()
    {
        if(render.sprite == null || BoardManager.instance.IsShifting)
        {
            return;
        }
        if (isSelected)
        {
            Deselect();
        }
        else
        {
            if(previousSelected == null)
            {
                Select();
            }
            else
            {
                //SwapSprite(previousSelected.render);
                //previousSelected.Deselect();
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    SwapSprite(previousSelected.render);
                    previousSelected.ClearAllMatches();
                    previousSelected.Deselect();
                    ClearAllMatches();
                }
                else
                {
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }
    public void SwapSprite(SpriteRenderer renderer2)
    {
        if(render.sprite == renderer2.sprite)
        {
            return;
        }
        Sprite tempSprite = renderer2.sprite;
        renderer2.sprite = render.sprite;
        render.sprite = tempSprite;
        SFXManager.instance.PlaySFX(Clip.Swap);
    }
    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for(int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
           return adjacentTiles;
    }
    private List<GameObject> FindMatch(Vector2 castDir)
    {
        // This method accepts a Vector2 as a parameter, which will be the direction all raycasts will be fired in.
        List<GameObject> matchingTiles = new List<GameObject>(); // Create a new list of GameObjects to hold all matching tiles.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // Fire a ray from the tile towards the castDir direction.
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        { // Keep firing new raycasts until either your raycast hits nothing, or the tiles sprite differs from the returned object sprite. If both conditions are met, you consider it a match and add it to your list.
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles; // Return the list of matching sprites.
    }

    private void ClearMatch(Vector2[] paths) // Take a Vector2 array of paths; these are the paths in which the tile will raycast.
    {
        List<GameObject> matchingTiles = new List<GameObject>(); // Create a GameObject list to hold the matches.
        for (int i = 0; i < paths.Length; i++) // Iterate through the list of paths and add any matches to the matchingTiles list.
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2) // Continue if a match with 2 or more tiles was found. 2 matching tiles is enough here, that’s because the third match is your initial tile.
        {
            for (int i = 0; i < matchingTiles.Count; i++) // Iterate through all matching tiles and remove their sprites by setting it null.
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true; // Set the matchFound flag to true.
        }
    }
    

    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;
        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;
            StopCoroutine(BoardManager.instance.FindNullTiles());
            StartCoroutine(BoardManager.instance.FindNullTiles());
            SFXManager.instance.PlaySFX(Clip.Clear);
            GUIManager.instance.MoveCounter--;
        }
    }





}