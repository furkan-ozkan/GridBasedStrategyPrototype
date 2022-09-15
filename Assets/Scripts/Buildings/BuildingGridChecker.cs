using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

    /// <summary>
    /// Every building has its own grid checkers, all of them has this script.
    /// and they r controlling ground grids empty or not.
    /// </summary>
    public class BuildingGridChecker : MonoBehaviour
    {
        #region Vars

        public bool isEmpty = true;
        public GameObject grid;

        #endregion

        #region Triggers

        /// <summary>
        /// After leaves on a grid this func. is working
        /// updating his color and update its own var.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<GridBlock>())
            {
                isEmpty = true;
                ChangeColor(other.GetComponent<SpriteRenderer>(), new Color(1,1,1,.9f));
                grid = null;
            }
        }

        /// <summary>
        /// while stay in a grid, updating its color and isEmpty var.
        /// </summary>
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<GridBlock>())
            {
                grid = other.gameObject;
                // If Ground grid isEmpty == false
                if (!other.GetComponent<GridBlock>().isEmpty)
                {
                    isEmpty = false;
                    ChangeColor(other.GetComponent<SpriteRenderer>(), new Color(1f, 0, 0, .5f));
                }
                // If Ground grid isEmpty == true
                else
                {
                    isEmpty = true;
                    ChangeColor(other.GetComponent<SpriteRenderer>(), new Color(0, 1, 0, .8f));
                }
            }
        }

        #endregion

        #region Helper Funcs. (Color Change)

        public void ChangeColor(SpriteRenderer renderer,Color color) => DOTweenModuleSprite.DOColor(renderer, color, .25f);

        #endregion
    }