import { Page } from "../../General/Page";
import { Link, useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { confirmUserEmail } from "../../../Api/IdentityData";
import { PageTitle } from "../../General/PageTitle";

export const EmailConfirmedPage = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token") || "";
  const email = searchParams.get("email") || "";
  const [loading, setLoading] = useState(true);
  const [completed, setCompleted] = useState(false);

  useEffect(() => {
    if (token !== "" && email !== "") {
      const doConfirmUserEmail = async (email: string, token: string) => {
        let result = await confirmUserEmail(email, token);
        if (result) setCompleted(true);
        setLoading(false);
      };
      doConfirmUserEmail(email, token);
    }
  }, [token, email]);

  return (
    <Page>
      {loading ? (
        <div>Загрузка...</div>
      ) : (
        <div>
          {!completed ? (
            <div>Что-то пошло не так!</div>
          ) : (
            <div>
              <PageTitle>Почта успешно подтверждена!</PageTitle>
              <Link to="/login">Войти в систему</Link>
            </div>
          )}
        </div>
      )}
    </Page>
  );
};
