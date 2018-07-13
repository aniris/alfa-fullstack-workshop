import axios from "axios";

// написать функции, которые достают карты с обработкой ошибок, транзакции по карте,
// добавляют карты и совершают переводы

axios.get("http://localhost/api/cards/").then(result => {
  console.log(result.data);
});
