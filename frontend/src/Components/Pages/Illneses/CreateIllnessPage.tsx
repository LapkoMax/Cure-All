import { useSearchParams } from "react-router-dom";
import { Page } from "../../General/Page";
import { CreateIllness } from "../../Illness/CreateIllness";

export const CreateIllnessPage = () => {
  const [searchParams] = useSearchParams();
  const returnUrl = searchParams.get("returnUrl") || "";

  return (
    <Page title="Добавить новое заболевание:">
      <CreateIllness returnUrl={returnUrl ? returnUrl : ""} />
    </Page>
  );
};
