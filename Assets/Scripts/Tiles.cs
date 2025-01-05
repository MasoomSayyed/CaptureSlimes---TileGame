using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    [SerializeField] private GameObject selectionHighlight;
    [SerializeField] private GameObject selectionBox;
    private bool isSelected = false;
    public static Tiles selectedTile; // Static reference to the currently selected tile

    [SerializeField] private AudioClip hoverClip;

    private void OnMouseEnter()
    {
        AudioSource.PlayClipAtPoint(hoverClip, Camera.main.transform.position, 1f);
        selectionHighlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        selectionHighlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (selectedTile != null && selectedTile != this)
        {
            // Deselect the currently selected tile
            selectedTile.DeselectTile();
        }

        if (!isSelected)
        {
            SelectTile();
            Debug.Log(selectedTile);

            TileGrid.Instance.StoreRefForTiles(selectedTile);
        }
        else
        {
            DeselectTile();
        }

    }

    private void SelectTile()
    {
        isSelected = true;
        selectionBox.SetActive(true); // Enable the selection box
        selectedTile = this; // Set this tile as the currently selected tile
    }

    private void DeselectTile()
    {
        isSelected = false;
        selectionBox.SetActive(false); // Disable the selection box
        selectedTile = null; // Clear the currently selected tile reference
    }


}