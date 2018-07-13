import axios from "axios";

// http://alfa-test-api.dev.kroniak.net/api/v1/products/
// Необходимо с помощью promises достать все продукты и посчитать кол-во
// В итоговом массиве продуктов реализовать свою реализацию метода map
// и увеличить каждому продукту цену на 10%

axios
  .get("http://alfa-test-api.dev.kroniak.net/api/v1/products/")
  .then(result => {
    console.log(result.data);
  });
