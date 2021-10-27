extends Area2D

export var speed = 50.0
var screen_size = Vector2.ZERO
var turn_amount = 7.5


func _ready():
	screen_size = get_viewport_rect().size

func _process(delta):
	var direction = Vector2.ZERO
	if Input.is_action_pressed("ui_up"):
		direction.y -= 1
	if Input.is_action_pressed("ui_down"):
		direction.y += 1
	if Input.is_action_pressed("ui_left"):
		direction.x -= 1
	if Input.is_action_pressed("ui_right"):
		direction.x += 1
	if direction.length() > 1:
		direction = direction.normalized()
	
	
	
		
	position += direction * speed * delta
	
	position.y = clamp(position.y, 8, screen_size.y - 8)
	position.x = clamp(position.x, 8, screen_size.x - 8)
