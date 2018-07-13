// 1
var user = {
  firstName: "Вася",
  sayHi: function() {
    console.log(this.firstName);
  }
};

var admin = user;
admin.firstName = "admin";
user.sayHi();

// 2
var user = { firstName: "Вася" };
var admin = { firstName: "Админ" };

function func() {
  console.log(this.firstName);
}

user.f = func;
admin.g = func;

user.f();
admin.g();

// 3
var user = {
  name: "Вася",
  hi: function() {
    console.log(this.name);
  },
  bye: function() {
    console.log("Пока");
  }
};

user.hi();

(user.name == "Вася" ? user.hi : user.bye)();

// call
// func.call(context, arg1, arg2, ...);

function showFullName() {
  console.log(this.firstName + " " + this.lastName);
}

var user = {
  firstName: "Василий",
  lastName: "Петров"
};

showFullName.call(user);

// потеря контекста
var user = {
  firstName: "Вася",
  sayHi: function() {
    console.log(this.firstName);
  }
};

var f = user.sayHi.bind(user);
setTimeout(f, 1000);

