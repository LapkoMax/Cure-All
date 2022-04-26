describe("Patient check", () => {
  beforeEach(() => {
    cy.visit("/");
  });
  it("When patient authorize, main page should appear", () => {
    cy.contains("Вход в аккаунт");

    cy.findByLabelText("Имя пользователя или эл. почта:").type("TestPatient");
    cy.findByLabelText("Пароль:").type("Password123!");

    cy.contains("Войти").click();

    cy.contains("Добро пожаловать!");
  });

  it("When patient go to notifications page, notifications page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestPatient",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.findByText("Уведомления").click();

    cy.url().should("include", "notifications");
  });

  it("When patient go to doctors list, doctors list page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestPatient",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.wait(500);
    cy.findByText("Список докторов").click();

    cy.contains("Параметры фильтрации:");
    cy.contains("Список докторов");
  });

  it("When patient go to patient card, patient card page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestPatient",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.wait(500);
    cy.findByText("Ваша карта").click();

    cy.contains("Карта пациента");
    cy.contains("Посещения пациента:");
  });

  it("When patient go to patient profile, patient profile page should appear", () => {
    cy.get("body").then((body) => {
      if (
        body.find("button[type=submit]")[0] &&
        body.find("button[type=submit]")[0].innerText === "Войти"
      ) {
        cy.findByLabelText("Имя пользователя или эл. почта:").type(
          "TestPatient",
        );
        cy.findByLabelText("Пароль:").type("Password123!");

        cy.contains("Войти").click();
      }
    });

    cy.contains("Добро пожаловать!");

    cy.wait(500);
    cy.findByText("TestPatient").click();

    cy.contains("ТестПациент");
    cy.contains("testPatient@");
  });
});
