extends Area2D

export var speed = 400;
var isTouchingField = false
var fieldTouched = 0
var screen_size

signal plant

func _ready() -> void:
	screen_size = get_viewport_rect().size
	
func _process(delta: float) -> void:
	var velocity = Vector2.ZERO
	if Input.is_action_pressed("move_right"):
		velocity.x += 1
	if Input.is_action_pressed("move_left"):
		velocity.x -= 1
	if Input.is_action_pressed("move_down"):
		velocity.y += 1
	if Input.is_action_pressed("move_up"):
		velocity.y -= 1
	
	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)
	
	if isTouchingField && Input.is_action_just_pressed("plant"):
		emit_signal("plant", fieldTouched)
		

func touchingField(isTouching, fieldNumber):
	isTouchingField = isTouching
	fieldTouched = fieldNumber
