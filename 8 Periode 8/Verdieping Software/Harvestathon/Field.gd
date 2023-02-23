extends Area2D

signal changeTouch
export var fieldNumber = 0;
var currentAnimation = 0;
onready var playerNode = $"../Player"
var isBeingTouched = false
var timer:float = 0
var growTime := 4.0

enum fieldStates { empty, planted, grown, rotten }
var currentState = fieldStates.empty

func _ready() -> void:
	playerNode.connect("plant", self, "plant")

func _process(delta: float) -> void:
	timer -= delta
	if isBeingTouched:
		$AnimatedSprite.frame = 1 + currentAnimation
	else:
		$AnimatedSprite.frame = 0 + currentAnimation
	if timer < 0:
		if currentState == fieldStates.planted:
			currentAnimation = 4
			timer = growTime
			currentState = fieldStates.grown
		elif currentState == fieldStates.grown:
			currentAnimation = 6
			currentState = fieldStates.rotten


func changeTouch(_body, isTouching):
	emit_signal("changeTouch", isTouching, fieldNumber)
	isBeingTouched = isTouching

func plant(targetField):
	if targetField == fieldNumber:
		if currentState == fieldStates.empty:
			timer = growTime
			currentAnimation = 2
			currentState = fieldStates.planted
		elif currentState == fieldStates.grown:
			growTime *= 0.95
			currentAnimation = 0
			currentState = fieldStates.empty
