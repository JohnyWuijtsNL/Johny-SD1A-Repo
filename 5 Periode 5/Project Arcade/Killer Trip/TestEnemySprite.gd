extends Sprite
var rotate_speed = 180
var interpolation_level = 0.5
var enemy_offset = Vector2(56, 40)

func _process(delta):
	rotation_degrees += rotate_speed * delta
	position = position.linear_interpolate(get_parent().get_parent().get_node("Player").position - enemy_offset, interpolation_level * delta)

