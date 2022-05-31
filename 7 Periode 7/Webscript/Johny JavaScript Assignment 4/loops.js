function buttonClick(buttonNumber) {
  for (var i = 0; i < 10; i++) {
    switch (buttonNumber) {
      case 0:
        console.log(i + 1);
        break;
      case 1:
        console.log(20 - i);
        break;
      case 2:
        console.log((i + 1) * 2);
        break;
      case 3:
        console.log(100 - i);
        break;
    }
  }
}
