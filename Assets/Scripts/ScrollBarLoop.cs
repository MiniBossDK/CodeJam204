using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Code was from the tutorial series: https://www.youtube.com/watch?v=9B7ahj1kaYs
//and: https://www.youtube.com/watch?v=jWbAaBEQpvE&t=318s
//and: https://www.youtube.com/watch?v=2m7pnTC0seo

public class ScrollBarLoop : MonoBehaviour
{

    public RectTransform panel; //to hold the scrollpanel
    public Button[] bttn; //to call the buttons
    public RectTransform center; //center to compare the distance for each button



    public float[] distance; // all buttons' distance to the center 
    public float[] distReposition;
    private bool dragging = false; //will be true while we drag the panel
    private int bttnDistance; // will hold the distance between the buttons
    private int minButtonNum; // to hold the number of the button, with smallest distance to center
    private int bttnLength;

    // Start is called before the first frame update
    void Start()
    {
        bttnLength = bttn.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];

        //get distance between buttons
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            // takes the distance between the center and the button so if the button is in negative position it will display a negative value 

            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            //the distance have the same values as the distance reposition but now we have it in absolute numbers
            distance[i] = Mathf.Abs(distReposition[i]);

            // these 2 if statements is the lines of code that make the scroll bar infinite and make a loop
            // because it takes the center of the screen which is 0 on the x axes and y axes, but because we have stopped if from doing it vertical 
            // it will only do it horizontally. so the magic numbers 5 and -5 is the lengths that the scroll bar "buttons" or icons should apear in
            // so when they appear only between a positive and negative axes and then we have put a mask which covers the parts we dont want to show
            // which makes it only show 3-3.5 icons at a time.


            //if the button gets out of the border that is preset; 5 to -5 which is also the distance from the center
            //it will take its "curX"/"current x position" + (the bttnLength and multiply it with the bttndistance)
            //it will then move the button with the values the variable gives it and push it to the side, while still staying at the same y position.
            if (distReposition[i] > 5)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchorPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchorPos;
            }

            if (distReposition[i] < -5)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchorPos = new Vector2(curX - (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchorPos;
            }
        }

        float minDistance = Mathf.Min(distance); // get the minimum distance


        //we are checking for which button have the same distance as the minimum distance         
        for (int a = 0; a < bttn.Length; a++)
        {
            // if the minDistance is = to distance[a] so we are going to loop between each button to
            // see which button have the same distance as the minimum distance. to we can get to see which button is holding the minimum distance.
            if (minDistance == distance[a])
            {
                //minbuttonNum holds the number of the button that have the smallest distance to the center
                minButtonNum = a;
            }
        }
        // if we have released the mouse
        if (!dragging)
        {
            // LerpToBttn takes the the bttn to another location if it scrolled out of the border that was made above
            // the bttn then gets set closer to another bttn but on the other side of the scrollwheel.
            LerpToBttn(bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }

    void LerpToBttn(float position)
    {
        // this function lerps the float of the newX by the position value of the panel
        // the 0f is the actual movement speed of the panel
        // which is 0 because if it is above 0 it will move everytime we scroll on it
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 0f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        // this anchor the position of the panel to a new position
        panel.anchoredPosition = newPosition;
    }
    // start drag and end drag just see if the drag is true or false
    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}