QuadTree is data structure based on nodes, where each internal node has four children. General idea of alghoritm is to increase acuracy by dividing the space into four equal parts, and then divide the succesive quarters into new parts until desired result is obtained.

What it does: 
The goal of alghoritm in program is to create a structural grid (continious object/shape representation in the form of smaller calculation areas called elements) which is focused (denser) on specific, ineresting for user objects. 

How it works:
Area is initially divided into four parts, then the main node is passed to recursive method that divides next nodesonly in areas that are not entirely empty or completely filles with object parts. This way grid will be the most focused on the boundaries of the elements 
(in many cases boundaries are most important elements). User is able to choose size of smallest gained elements. As the result program returns connection matrix which is list of created elements and coordinates of their vertices.

Application:
QuadTree method have wide range of applications in many areas, e.g. examination of material structures, Graphics, collision detection, mesh generation.
