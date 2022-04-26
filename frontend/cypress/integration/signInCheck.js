describe("Sign in check", () => {
  beforeEach(() => {
    cy.visit("/");
  });
  it("When trying sign in with blank inputs, error messsage should appear", () => {
    cy.contains("Вход в аккаунт");

    cy.contains("Войти").click();

    cy.contains("Логин обязателен!");
    cy.contains("Пароль обязателен!");

    cy.findByLabelText("Имя пользователя или эл. почта:").type("A");
    cy.findByLabelText("Пароль:").type("A");

    cy.contains("Войти").click();

    cy.contains("Неверные данные пользователя!");
  });
});
