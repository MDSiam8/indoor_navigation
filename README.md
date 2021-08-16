# indoor_navigation
Spatial computing based indoor navigation. 
# What is this?
This is an Android app that uses ARCore to provide an indoor navigation solution. 

# How does it work?
First, users are asked for camera input. This is needed because that's the input required for ARCore. It uses the camera input for the transformations. 

Users are then asked for their chosen destination. After plugging it in, the system checks for its validity, and if it's valid, users are guided to a 
"location synchronization" screen. You may be wondering, why not just use a GPS? GPS wouldn't be valid in situations like this due to their relatively large margin of error, 
when viewed in the scope of navigation within a building. That's why these location synchronization points are required, so that the app knows where you are, 
and can use spatial computing rather than GPS to guide you to your chosen destination. 

After users input a correct location synchronization point, the app runs a script to create a path to the destination point, and tells the user when they can start moving.
Once users arrive at their destination, a message will appear to notify them.  

# How did I make this?
First off, I used Unity 2019 LTS version. Why? Because newer versions of Unity don't support the ARCore SDK. I was more familiar with the ARCore SDK than ARFoundations, 
so I had to sacrifice my Unity version in order to still be able to develop with ARCore. Plus, it has better documentation, in my opinion. 

ARCore SDK has a class called Frame.Pose. This refers to the calculated transformation of the ARDevice (i.e. the device that is running the program and feeding in the camera input).
I used this class to dictate the movements of a gameobject meant to represent the player, done through setting its position equal to the transformation of the ARDevice 
as well as the offset position for the player (representative of the location synchronization position).

For the rotations, it was a bit more nuanced. This is a bit difficult to word, but I'll try my best. 
ARCore initializes its tracking with whatever is in front of it. So, because of that, it doens't matter what the device's initial rotation is. If the device (and the corresponding 
gameobject representing the player) are rotated, for example, 90 degrees to the right, ARCore will not take that into consideration. So, if you move 90 degrees to the left, and then start moving forward 
(aka you turn left to face straight and start moving straight), it will create an awkward and incorrect positioning in the app, where going straight forward is not what you see in the ARCore calculations, even though that's what you're doing IRL.
Alternatively, if you decide to move forward while you're still in that 90 degree position, it will translate you forward, but not towards wherever you're facing. For an analogy, imagine a car parked sideways, facing West. You're in the drivers seat, and 
when you hit the acceleration pedal, you expect to move further along West. But instead, it moves you North, even as your car is sideways facing West.

In short, ARCore doesn't anticipate any rotations other than looking forward for the location synchronization positions of gameobjects that represent the user. But this creates problems when 
placing synchronization points around the building due to the wall placements. You typically wouldn't be able to place the synchronization points on some walls, depending on how the map was imported 
into Unity. 

I dealt with this issue by rotating the map whenever necessary. One thing to note here is that the NavMesh path also rotates with it, which typically doesn't happen. 
The reason it worked out so smoohtly for me is that I imported a high level Navemsh component, called NavMeshSurface, and attached it to the rotating map. That way,
the NavMesh path rotated along with it. 

And speaking of NavMesh, it doesn't automatically find the obstacles in a flat map that may be imported. Unless it's a 3d map, you're going to have to create obstacles 
manually using basic shapes like cubes and spheres. After you create the obstacles, you have to bake the NavMesh again to factor in the obstacles, if you hadn't already, and then you could simply just set the 
obstacles to be inactive afterwards beacause they won't be needed again, unless you bake it again and the factored in obstacles disappear from the Navmesh path. 

Then after creating the path drawing system, I updated the user interface. I used Century Gothic for the font because of it's pleasing and just generally friendly aesthetic. 

# Any tips for others trying to make a similar app?
YES! Before you hit the build button, double check whether or not your ARCore script is activated. This will save you from a lot of unnecessary frustrations :').
Also, consider using high-level NavMesh components; these do not come installed with Unity, so you have to install them yourself. 
Another tip: if you want to scale things in Unity, such as a map, you should know that in Unity, one unit means one meter. So, for example, the way I scaled my map into the 
game was that I summoned a primitive cube, with scale 1 x 1 x 1. Then I imported in my scaled map, which was on graph paper where 1 unit was equal to 1 meter. I stretched out that map both ways, both the length and the width, until one of the cubes on the graph paper fit 1 to 1 with the summoned primitive cube. Then, I deleted the primitive cube becuase it was not needed anymore. 
