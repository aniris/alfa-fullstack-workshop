// Создаётся объект promise
let promise = new Promise((resolve, reject) => {
  setTimeout(() => {
    // переведёт промис в состояние fulfilled с результатом "result"
    resolve("result");
  }, 1000);
});

// promise.then навешивает обработчики на успешный результат или ошибку
promise.then(
  result => {
    // первая функция-обработчик - запустится при вызове resolve
    console.log("Fulfilled: " + result);
  },
  error => {
    // вторая функция - запустится при вызове reject
    console.log("Rejected: " + error);
  }
);

import axios from "axios";

axios
  .get("/article/promise/user.json")
  // 1. Получить данные о пользователе в JSON и передать дальше
  .then(response => {
    let user = JSON.parse(response.data);
    return user;
  })
  // 2. Получить информацию с github
  .then(user => {
    return axios.get(`https://api.github.com/users/${user.name}`);
  })
  // 3. Вывести аватар
  .then(result => {
    githubUser = JSON.parse(result.data);

    let img = new Image();
    img.src = githubUser.avatar_url;
    img.className = "promise-avatar-example";
    document.body.appendChild(img);
  });
