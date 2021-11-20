# Gatherer Simulation

A .zip file has been included if you would like to run the Gatherer Simulation. It includes a .exe that runs the unity application.

<video src="photos\2021-11-20 16-23-48_Trim.mp4"></video>

#### Four States

**![image-20211120153656166](photos\image-20211120153656166.png)SEARCHING** - Gatherer is taking a random walk across the grid, searching for food.

![image-20211120153837061](photos\image-20211120153837061.png)**SUCCESS** - Gatherer has found food and is either brining it back to the nest, or going back to the food source

**![image-20211120153754963](photos\image-20211120153754963.png)FAIL** - Gatherer has failed to find food within the number of steps specified. Will return back to the nest.

**![image-20211120153824422](photos\image-20211120153824422.png)FOLLOW_SUCCESS** - After failing to find food the Gatherer will select the shortest successful path and follow it to the food source.



#### UI 

![image-20211120154126622](photos\image-20211120154126622.png)**Movement Selector** - Red squares are the directions the gatherer can move on the grid. Click to toggle the squares on/off. 

*Only influences random walk, not when gatherers are following their path.*

**Space bar** - Will reset the simulation.

**ESC** - Will exit the simulation.

#### Tile Selection

**Two options for tile selection**

1. **Random Walk** - Used when the gatherer is searching for food. Randomly select a tile according to the specified movement chosen in the movement selector. *Ex. Using the chosen movement above.* Green tiles are the options it can choose from.

![image-20211120155341675](photos\image-20211120155341675.png)

2. **Check If Neighbours Have Food** - Used when gatherer is successful in its search for food and is heading back to the food source. It will check all neighbouring tiles on its path back to the food, and where it previously found the food.

   *Ex. Purple triangle is the nest it brings the food back to. Yellow star is the previously picked up food. Green squares the neighbouring tiles it is checking for while traversing the grid. Blue arrow shows it found another food source on one of the tiles neighbour.*

   ![image-20211120155956673](photos\image-20211120155956673.png)

   *Shows that path will be cut short when the gatherer encounters a closer food source.*

   ![image-20211120160222249](photos\image-20211120160222249.png)



#### Game Loop

![image-20211120162122009](photos\image-20211120162122009.png)