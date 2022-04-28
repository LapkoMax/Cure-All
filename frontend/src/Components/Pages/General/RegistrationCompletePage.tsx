import { Page } from "../../General/Page";
import { Link } from "react-router-dom";

export const RegistrationCompletePage = () => (
  <Page title="Вы успешно зарегистрированы">
    <div>
      Подтвердите электронную почту, перейдя по ссылке в письме, отправленном на
      неё. Если вы не видите письма в течении 15 минут, то попробуйте проверить
      раздел "спам".
    </div>
    <Link to="/login">Я подтвердил почту!</Link>
  </Page>
);
