describe("Regestration check", () => {
  beforeEach(() => {
    cy.visit("/");
  });
  it("When trying register patient with wrong data in inputs, error messsages should appear", () => {
    cy.contains("Вход в аккаунт");

    cy.contains("Зарегистрироваться").click();

    cy.contains("Зарегистрироваться как:");

    cy.contains("Пациент").click();
    cy.get("form").submit();

    cy.contains("Имя обязательно!");
    cy.contains("Фамилия обязательна!");
    cy.contains("Имя пользователя обязательно!");
    cy.contains("Почта обязательна!");
    cy.contains("Дата рождения обязательна!");
    cy.contains("Номер телефона обязателен!");
    cy.contains("Почтовый код обязателен!");
    cy.contains("Страна обязательна!");
    cy.contains("Город обязателен!");
    cy.contains("Пароль обязателен!");
    cy.contains("Подтверждение пароля обязательно!");

    cy.findByLabelText("Имя").type("A", { force: true });
    cy.findByLabelText("Фамилия").type("A", { force: true });
    cy.findByLabelText("Имя пользователя").type("A", { force: true });
    cy.findByLabelText("Эл. почта").type("A", { force: true });
    cy.findByLabelText("Номер телефона").type("A", { force: true });
    cy.findByLabelText("Почтовый код").type("A", { force: true });
    cy.findByLabelText("Страна").type("A", { force: true });
    cy.findByLabelText("Город").type("A", { force: true });
    cy.findByLabelText("Пароль").type("A", { force: true });
    cy.findByLabelText("Подтвердите пароль").type("A", { force: true });

    cy.contains("Имя должно состоять минимум из 2 символов!");
    cy.contains("Фамилия должна состоять минимум из 2 символов!");
    cy.contains("Имя пользователя должно состоять минимум из 2 символов!");
    cy.contains("Неверный формат адреса почты!");
    cy.contains("Дата рождения обязательна!");
    cy.contains("Неверный формат номера!");
    cy.contains("Код должен состоять из 5 цифр!");
    cy.contains("Название страны не может быть меньше 3 символов!");
    cy.contains("Название города не может быть меньше 3 символов!");
    cy.contains("Пароль должен состоять минимум из 8 символов!");

    cy.findByLabelText("Имя").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Фамилия").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Имя пользователя").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Страна").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Город").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Пароль").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );

    cy.contains("Имя должно состоять максимум из 50 символов!");
    cy.contains("Фамилия должна состоять максимум из 50 символов!");
    cy.contains("Имя пользователя должно состоять максимум из 20 символов!");
    cy.contains("Название страны не может быть больше 50 символов!");
    cy.contains("Название города не может быть больше 50 символов!");
    cy.contains("Пароль должен содержать минимум 1 цифру");
  });

  it("When trying register doctor with wrong data in inputs, error messsages should appear", () => {
    cy.contains("Вход в аккаунт");

    cy.contains("Зарегистрироваться").click();

    cy.contains("Зарегистрироваться как:");

    cy.contains("Доктор").click();
    cy.get("form").submit();

    cy.contains("Имя обязательно!");
    cy.contains("Фамилия обязательна!");
    cy.contains("Дата рождения обязательна!");
    cy.contains("Дата начала работы обязательна!");
    cy.contains("Почтовый код обязателен!");
    cy.contains("Номер телефона обязателен!");
    cy.contains("Страна обязательна!");
    cy.contains("Город обязателен!");
    cy.contains("Специализация обязательна!");
    cy.contains("Продолжительность приёма обязательна!");
    cy.contains("Время начала рабочего дня обязательно!");
    cy.contains("Время конца рабочего дня обязательно!");
    cy.contains("Время начала обеденного перерыва обязательно!");
    cy.contains("Время конца обеденного перерыва обязательно!");
    cy.contains("Номер лицензии обязателен!");
    cy.contains("Адрес работы обязателен!");
    cy.contains("Имя пользователя обязательно!");
    cy.contains("Почта обязательна!");
    cy.contains("Пароль обязателен!");
    cy.contains("Подтверждение пароля обязательно!");

    cy.findByLabelText("Имя").type("A", { force: true });
    cy.findByLabelText("Фамилия").type("A", { force: true });
    cy.findByLabelText("Почтовый код").type("A", { force: true });
    cy.findByLabelText("Номер телефона").type("A", { force: true });
    cy.findByLabelText("Страна").type("A", { force: true });
    cy.findByLabelText("Город").type("A", { force: true });
    cy.findByLabelText(
      "Примерная продолжительность вашего приёма(в минутах):",
    ).type("1", { force: true });
    cy.findByLabelText("Номер лицензии").type("A", { force: true });
    cy.findByLabelText("Адрес работы").type("A", { force: true });
    cy.findByLabelText("Имя пользователя").type("A", { force: true });
    cy.findByLabelText("Эл. почта").type("A", { force: true });
    cy.findByLabelText("Пароль").type("A", { force: true });
    cy.findByLabelText("Подтвердите пароль").type("A", { force: true });

    cy.contains("Имя должно состоять минимум из 2 символов!");
    cy.contains("Фамилия должна состоять минимум из 2 символов!");
    cy.contains("Дата рождения обязательна!");
    cy.contains("Дата начала работы обязательна!");
    cy.contains("Код должен состоять из 5 цифр!");
    cy.contains("Неверный формат номера!");
    cy.contains("Название страны не может быть меньше 3 символов!");
    cy.contains("Название города не может быть меньше 3 символов!");
    cy.contains("5 минут - минимальное время");
    cy.contains("Имя пользователя должно состоять минимум из 2 символов!");
    cy.contains("Неверный формат адреса почты!");
    cy.contains("Пароль должен состоять минимум из 8 символов!");

    cy.findByLabelText("Имя").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Фамилия").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Страна").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Город").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText(
      "Примерная продолжительность вашего приёма(в минутах):",
    ).type("100", { force: true });
    cy.findByLabelText("Имя пользователя").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );
    cy.findByLabelText("Пароль").type(
      "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      { force: true },
    );

    cy.contains("Имя должно состоять максимум из 50 символов!");
    cy.contains("Фамилия должна состоять максимум из 50 символов!");
    cy.contains("Название страны не может быть больше 50 символов!");
    cy.contains("Название города не может быть больше 50 символов!");
    cy.contains("45 минут - максимальное время");
    cy.contains("Имя пользователя должно состоять максимум из 20 символов!");
    cy.contains("Пароль должен содержать минимум 1 цифру");
  });
});
