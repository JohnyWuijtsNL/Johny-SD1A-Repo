extends Sprite

func _process(delta):
	look_at(get_parent().get_node("LookDirectionInterpolate").get_global_position())
