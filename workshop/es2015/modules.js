// default name export
export default () => ({
  name: "admin",
  password: "admin",
  login() {
    console.log("hello");
  }
});

//named export
export const summa = a => a + 1;

const summator = b => b + 1;

export { summator };


