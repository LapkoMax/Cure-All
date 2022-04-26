describe("Patient check", () => {
  beforeEach(() => {
    cy.visit("/");
  });
  it("When doctor authorize, main page should appear", () => {
    cy.contains("Вход в аккаунт");

    cy.findByLabelText("Имя пользователя или эл. почта:").type("TestDoctor");
    cy.findByLabelText("Пароль:").type("Password123!");

    cy.contains("Войти").click();

    cy.contains("Добро пожаловать!");
  });

  it("When doctor go to notifications page, notifications page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestDoctor",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.findByText("Уведомления").click();

    cy.url().should("include", "notifications");
  });

  it("When doctor go to appointments page, appointments page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestDoctor",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.wait(500);
    cy.findByText("Ваши посещения").click();

    cy.contains("Посещения для даты:");
  });

  it("When doctor go to doctor profile, doctor profile page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestDoctor",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.wait(500);
    cy.findByText("TestDoctor").click();

    cy.contains("ТестДоктор");
    cy.contains("testDoctor@");
  });
});
