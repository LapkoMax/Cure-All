import { useState } from "react";
import { FormLabel } from "react-bootstrap";
import { resetPasswordRequest } from "../../../Api/IdentityData";
import { PrimaryButton } from "../../../Styles/Common/Buttons";
import {
  FieldContainer,
  FieldError,
  FieldInput,
  FieldSuccess,
} from "../../../Styles/Common/FieldStyles";
import { Page } from "../../General/Page";

export const ForgotPasswordPage = () => {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async () => {
    if (email !== "") {
      const doSendResetPasswordRequest = async (email: string) => {
        var result = await resetPasswordRequest(email);
        setError(result);
        if (result === "") setSuccess(true);
      };
      doSendResetPasswordRequest(email);
    }
  };

  return (
    <Page title="Восстановление пароля">
      {success && (
        <FieldSuccess>
          Письмо отправлено на указанный адрес. Если вы не смогли его найти, то
          проверьте раздел спам.
        </FieldSuccess>
      )}
      <form onSubmit={handleSubmit}>
        <FieldContainer className="col-12 row">
          <FormLabel>Введите адрес эл. почты:</FormLabel>
          <div className="col-8 row">
            <FieldInput
              type="email"
              onChange={(e: any) => {
                setEmail(e.target.value);
              }}
            />
          </div>
          <FieldError>{error}</FieldError>
        </FieldContainer>
        <PrimaryButton type="submit">Отправить письмо</PrimaryButton>
      </form>
    </Page>
  );
};
