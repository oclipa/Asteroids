using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenWrapper {

    /// <summary>
    /// Checks if the specified transform with the specified colliderRadius
    /// is outside the bounds of the screen and, if so, moves it to the other 
    /// side of the screen.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="colliderRadius">Collider radius.</param>
    public static void ScreenWrap(Transform transform, float colliderRadius)
    {
        // get the current position of the transform
        Vector3 objPosition = transform.position;

        float objMinX = objPosition.x - colliderRadius;
        float objMaxX = objPosition.x + colliderRadius;
        float objMinY = objPosition.y - colliderRadius;
        float objMaxY = objPosition.y + colliderRadius;

        // test if off left side of screen
        if (objMinX < ScreenUtils.ScreenLeft)
        {
            objPosition.x = ScreenUtils.ScreenRight + colliderRadius;
        }

        // test if off right side of screen
        if (objMaxX > ScreenUtils.ScreenRight)
        {
            objPosition.x = ScreenUtils.ScreenLeft - colliderRadius;
        }

        // test if off bottom of screen
        if (objMinY < ScreenUtils.ScreenBottom)
        {
            objPosition.y = ScreenUtils.ScreenTop + colliderRadius;
        }

        // test if off top of screen
        if (objMaxY > ScreenUtils.ScreenTop)
        {
            objPosition.y = ScreenUtils.ScreenBottom - colliderRadius;
        }

        // set the new position of the transform
        transform.position = objPosition;
    }
}
