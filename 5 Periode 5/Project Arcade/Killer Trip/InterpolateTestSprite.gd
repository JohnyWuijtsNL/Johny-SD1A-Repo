extends Sprite
var original_position

func _ready():
	original_position = position

func _process(delta):
	var shoot_direction = Vector2.ZERO
	if Input.is_action_pressed("shoot_up"):
		shoot_direction.y -= 1
	if Input.is_action_pressed("shoot_down"):
		shoot_direction.y += 1
	if Input.is_action_pressed("shoot_left"):
		shoot_direction.x -= 1
	if Input.is_action_pressed("shoot_right"):
		shoot_direction.x += 1

	position = original_position + shoot_direction
