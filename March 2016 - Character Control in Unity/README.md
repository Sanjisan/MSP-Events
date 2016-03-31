# Getting Started

1. Download or update to the newest version of Unity **before** the workshop. I will not be waiting for downloads to complete.
2. Bring a laptop that is capable of running Unity, and have it fully charged. Assume no outlets.

# Using the Files
Please try and setup the project yourself by using the script files, and even better type them out by hand using them only for reference. The project files are included for comparing or if you are stuck.

# Creating the Controller
## Setting up the Environment and  the Character Model
1. Create a new 3D project.
2. Save a scene as *Main*.
3. Import character Assets from the Assets > Import Package menu. Import everything if you are unsure what to import. Will not really effect anything.
4. Insert a terrain GameObject from the GameObject > 3D Object menu.
5. Insert the third person player model by using the project explorer at the bottom of the screen. Most likely you will be in the *Asset* folder by default. So if you do not see a folder with the name *Asset* do not worry. That is fine. Navigate to *Asset > Standard Assets > Character > ThidPersonCharacter > Models* and drag the model somewhere onto the terrain.
6. Edit the transform (position) of the model on the screen to somewhere like (5, 0, 5) in the *Inspector* view.
7. Create a new material by clicking in some open space in the root of the Asset folder then *Create*.
8. Then in the inspector menu change the color by clicking on *Emission* and selecting the color of your choice.
9. Drag the material over the character model to set it.

## Setting up the Camera System
1. Edit Main Camera tag to *PlayerCamera*. Set tag in the Inspector. Rename it from Main Camera to PlayerCamera as well.

## Setting up Animations
1. Add a new *Animator* component to *Ethan*.
2. In the Asset folder create a new *AnimatorController* name it *PlayerAnimator*.
3. Select Ethan and add Player Controller to our Animator controller, and add *EthanAvatar* to the avatar section of the component.
4. Double click on the PlayerAnimator to open the animation window. 
5. Creating the Animation FSM *(bet you thought you saw the last of these, and that no one would use them in the real world)*:
    * Drag in the Idle animation from *ThirdPersonCharacter > Animation* folder to the animator FSM. It will be set to orange meaning it is the default animation at start of game.
    * Right click on an empty area in the animator window and add a new *Blend Tree* state. Name it *Movement*. Double click on the blend tree to edit it. Then rename it again to *Movement*.
    * In the Inspector view click add two more blend trees by clicking on the plus sign under motion.
    * Select our Movement blend tree, and add 3 float parameters, and one bool parameter. The floats named *Speed, Direction, AirVelocity* and bool *IsGrounded*
    * Set the Movement default motion parameter in the inspector window to *Speed*
    * For the two nested blend trees set their default parameters to *Direction*.
    * Rename the two blend trees. One to *Walk* one to *Run*.
    * Add 3 motion fields to Walk and Run. In the inspector view.
    * Add Walk Left, Walk, Walk Right to the Walk Blend Tree, and **exactly** in that order. Then uncheck *Automate Threshold*
    * Repeat the above for Run using the Run animations.
    * Set the thresholds for walking (-0.3, 0, 0.3)
    * Set the thresholds for running (
    * Go back to the main animation FSM and create a transition in both directions from Idle to Movement. 
    * On the transition to Movement set the parameter to Speed, and when **greater** than 0.3
    * On the transition away from Movement set hte paremter again to speed, and when **lesser** than 0.3.
    * Go back to base layer then add two more blend trees. Name one *Move_Left* and the other *Move_Right*. These will allow for us to move left or right without holding the forward button in addition to turning.
    * In Move_Left set the parameter to Direction and add the walking and running animations for left in that order. Then set the thresholds to -0.3, and -0.5.
    * Do the same for right, except with positive thresholds.
    * Back at the base layer create transitions to and all of the following states: Movement, Move_Right, Move_Left, and Idle, but not between Move_Left, and Move_Right.
    * For the transitions between Idle and Move_Left using Direction as the condition.
        * To Move_Left from Idle. When less than -0.3.
        * To Idle from Move_Left. When greater than -0.3.
    * For the transitions between Idle and Move_Right using Direction as the condition.
        * To Move_Right from Idle. When greater than 0.3.
        * To Idle from Move_Right. When lesser than 0.3.
    * For transitions between Move_Left and Movement.
        * From Move_Right to Movement create two parameters. One for speed set to greater than 0.3, and one for direction. Also greater than 0.3
        * From Movement to Move_Right create another two parameters. Speed < .3, and Direction > .3
    * For transitions between Move_Right and Movement.
        * From Move_Left to Movement create two parameters. Speed > .3, and Direction < -.3
        * From Movement to Move_Left create another two parameters. Speed < .3, and Direction < -.3
    * Create one more blend tree in the Base Layer, and name it *Jump*.
        * Three motion fields. Fall, Mid_Air, and JumpUp. There are multiples of each for right and left, but we are not going to focus on those. Set their thresholds to (-9, -5, 4) using the Speed parameter.
    * Create transitions from our other states to Jump.
        * For each transition to Jump set the parameter IsGrounded to false.
        * For the transition from Jump to Movement two parameters. Speed > .3, and IsGrounded = true.
        * For the transition from Jump to Idle. IsGrounded = true. Speed < .3.
        
## Scripting the Controller
1. Go to the *Edit > Project* menu. For both MouseX and MouseY
    * Gravity: 1
    * Dead Zone: 0.2
    * Sensitivity: 0.1
2. Create a new script in the Asset folder named *Helper*.
3. Create three more scripts *PlayerCharacter*, *PlayerMotor*, and *PlayerCamera*.
4. Open Helper script by double clicking on it. Copy the code into it.
5. Open PlayerController and copy the code into it.
6. Open PlayerCamera and copy the code into it.
7. Attach Player Character script to Ethan. Since we set requirements in the script they will automatically be included and will not need to be imported.
8. Open CharacterMotor script. Copy in the code.

## Done!
All that should be left now is to compile and run. 