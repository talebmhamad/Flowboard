import { useEffect, useRef } from "react";

export default function useFormioForm(formDesigner, formData) {

  const formRef = useRef(null);

  const formInstanceRef = useRef(null);

  const initializedRef = useRef(false);

  useEffect(() => {

    if (
      !formDesigner ||
      !formRef.current ||
      initializedRef.current
    ) return;

    let isMounted = true;

    const init = async () => {

      try {

        const { Formio } = await import("formiojs");

        if (!isMounted) return;

        const form = await Formio.createForm(
          formRef.current,
          formDesigner
        );

        form.submission = {
          data: formData || {}
        };

        formInstanceRef.current = form;

        initializedRef.current = true;

      } catch (err) {

        console.error(
          "Formio initialization error:",
          err
        );

      }

    };

    init();

    return () => {

      isMounted = false;

    };

  }, [formDesigner, formData]);

  return {
    formRef,
    formInstanceRef
  };

}